using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;

public class PlayerMovementMultiplayer : NetworkBehaviour
{
    

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float wallRunSpeed;


    public float groundDrag;

    //Jumping vars
    [Header("Jumping")]
    public float jumpForce;
    public float airMultiplier;

    //NewJumpSystem
    
    [SerializeField] private float _jumpButtonGracePeriod;
    private float? _lastGroundedTime;
    private float? _jumpButtonPressedTime;




    [Header("Ground Check")]
    [SerializeField] private GameObject playerObj;
    public LayerMask Ground;
    bool grounded;
    CapsuleCollider _capsuleCollider = null;
    [SerializeField] [Range(0.0f, 1.0f)] float _groundCheckRadiusMultiplier = 0.9f;
    [SerializeField] [Range(-0.95f, 1.05f)] float _groundCheckDistance = 0.05f;
    RaycastHit _groundCheckHit = new RaycastHit();

    public Transform orientation;
    


    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        freeze,
        walking,
        sprinting,
        wallrunning,
        Airborne
    }

    public bool wallrunning;
    public bool freeze;
    public bool activeGrapple;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        _capsuleCollider = playerObj.GetComponent<CapsuleCollider>();
    }

    // Control
    private void Start() {
        rb.freezeRotation = true;
    }

    private void Update() {
        // grounded = PlayerGroundCheck();
        // //Movement checkers and inputs
        // //MyInput();
        // SpeedController();
        // StateHandler();
    
        // Debug.Log("grounded: " + grounded);
        //Debug.Log("velocity: " + rb.velocity.magnitude);

        //Apply handle drag (friction)
        // if(grounded && !activeGrapple){
        //     rb.drag = groundDrag;
        // }
        // else{
        //     rb.drag = 0.1f;
        // }
    }

    public override void FixedUpdateNetwork() {
        grounded = PlayerGroundCheck();
        //Movement checkers and inputs
        //MyInput();
        SpeedController();
        StateHandler();
        if(grounded){
            _lastGroundedTime = Time.time;
        }

        if(GetInput(out NetworkInputData input)){
            horizontalInput = input.HorizontalInput;
            verticalInput = input.VerticalInput;
            MovePlayer();
            if(input.Jump){
                _jumpButtonPressedTime = Time.time;
            }
            if (Time.time - _lastGroundedTime <= _jumpButtonGracePeriod ){

            if (Time.time - _jumpButtonPressedTime <= _jumpButtonGracePeriod){
                _jumpButtonPressedTime = null;
                _lastGroundedTime = null;
                Jump();
            }
            }
        }
    }

    //Inputs and logic of movement
    // private void MyInput() {
    //     // Check inputs
    //     horizontalInput = Input.GetAxisRaw("Horizontal");
    //     verticalInput = Input.GetAxisRaw("Vertical");
       
    //    //Jump checks
    //     if(grounded){
    //         _lastGroundedTime = Time.time;
    //     }
    //     if(Input.GetKeyDown(jumpKey)){
    //         _jumpButtonPressedTime = Time.time;
    //     }

    //     if (Time.time - _lastGroundedTime <= _jumpButtonGracePeriod ){

    //         if (Time.time - _jumpButtonPressedTime <= _jumpButtonGracePeriod){
    //             _jumpButtonPressedTime = null;
    //             _lastGroundedTime = null;
    //             Jump();
    //         }
    //     }

    // }

    //State handler
    private void StateHandler(){
        if(GetInput(out NetworkInputData input)){
            // if (freeze){
            //     state = MovementState.freeze;
            //     moveSpeed = 0;
            //     rb.velocity = Vector3.zero;

            // }

            if(grounded){
                if(input.Sprint){
                    state = MovementState.sprinting;
                    moveSpeed = sprintSpeed;
                }
                else{
                    state = MovementState.walking;
                    moveSpeed = walkSpeed;
                }
            }else if(wallrunning){
                
                state = MovementState.wallrunning;
                moveSpeed = wallRunSpeed;
            
            }
            else{
                state = MovementState.Airborne;
            }
        }
    }

    private void MovePlayer(){

        if(activeGrapple) return;
        // calculate movement direction and add force to player rigid body
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }else if(!grounded){
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedController(){
        if(activeGrapple) return;
        // control player speed
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed. 
        if(flatVel.magnitude > moveSpeed){
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        
    }


    //Jump Section
    private void Jump(){
        
        //reset Y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        // jump
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private bool PlayerGroundCheck(){
        float sphereCastRadius = _capsuleCollider.radius * _groundCheckRadiusMultiplier;
        float sphereCastTravelDistance = _capsuleCollider.height * 0.5f - sphereCastRadius + _groundCheckDistance;
        

        return Physics.SphereCast(rb.position, sphereCastRadius, Vector3.down, out _groundCheckHit, sphereCastTravelDistance);
    }


    private bool enableMovementOnNextTouch;


     public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        //Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;

    
    }

    public void ResetRestrictions(){
        Debug.Log("Resetting restrictions");
        activeGrapple = false;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(enableMovementOnNextTouch)
        {
            enableMovementOnNextTouch = false;
            ResetRestrictions();
            
            GetComponent<Grappling>().StopGrapple();
        }
    }


     public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight)
    {
        float gravity = -20;
        Debug.Log(gravity);
        float displacementY = endPoint.y - startPoint.y;
        Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity) 
            + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

        return velocityXZ + velocityY;
    }
    

}
