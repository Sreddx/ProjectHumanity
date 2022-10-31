using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float wallRunSpeed;


    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump=true;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask Ground;
    bool grounded;

    public Transform orientation;
    
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [SerializeField] private Animator _animator;


    public MovementState state;

    public enum MovementState
    {
        idle,
        freeze,
        walking,
        sprinting,
        wallrunning,
        Airborne
    }

    public bool wallrunning;
    public bool freeze;
    public bool activeGrapple;

    // Control
    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update() {
        //Check ground with raycast
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, Ground);
        
        //Movement checkers and inputs
        MyInput();
        SpeedControler();
        StateHandler();
    
        // Debug.Log("grounded: " + grounded);
        //Debug.Log("velocity: " + rb.velocity.magnitude);

        //Apply handle drag (friction)
        if(grounded && !activeGrapple){
            rb.drag = groundDrag;
        }
        else{
            rb.drag = 0.1f;
        }
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    //Inputs and logic of movement
    private void MyInput() {
        // Check inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        //when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded){
            //Jump animation when walking
            if(state == MovementState.idle || state == MovementState.walking || state == MovementState.sprinting){
                _animator.SetTrigger("Jumping");
            }
            readyToJump = false;
            
            Jump();
            Invoke("ResetJump", jumpCooldown);
           
        }
    }

    //State handler
    private void StateHandler(){
        if (freeze){
            state = MovementState.freeze;
            moveSpeed = 0;
            rb.velocity = Vector3.zero;

        }

        
        if(grounded){
            if (horizontalInput == 0 && verticalInput == 0){
                state = MovementState.idle;
                moveSpeed = 0;
                _animator.SetBool("Walking", false);
                _animator.SetBool("Sprinting", false);

            }
            else if(Input.GetKey(sprintKey) && !(horizontalInput == 0 && verticalInput == 0)){
                state = MovementState.sprinting;
                moveSpeed = sprintSpeed;
                _animator.SetBool("Sprinting", true);
                _animator.SetBool("Walking", true);
            }
            else{
                state = MovementState.walking;
                _animator.SetBool("Walking", true);
                _animator.SetBool("Sprinting", false);
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

    private void MovePlayer(){

        if(activeGrapple) return;
        // calculate movement direction and add force to player rigid body
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControler(){
        if(activeGrapple) return;
        // control player speed
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed. 
        if(flatVel.magnitude > moveSpeed){
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        
    }

    private void OnDrawGizmos() {
        // draw ground check ray
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (playerHeight * 0.5f + 0.2f));
    }

    //Jump Section
    private void Jump(){
        
        //reset Y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        // jump
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump(){
        readyToJump = true;
       

        _animator.SetBool("Jumping", false);

    }


    private bool enableMovementOnNextTouch;


     public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight)
    {
        activeGrapple = true;

        velocityToSet = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);

        Invoke(nameof(ResetRestrictions), 3f);
    }

    private Vector3 velocityToSet;
    private void SetVelocity()
    {
        enableMovementOnNextTouch = true;
        rb.velocity = velocityToSet;

    
    }

    public void ResetRestrictions(){
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
