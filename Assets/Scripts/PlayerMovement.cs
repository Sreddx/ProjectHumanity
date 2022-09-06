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
        if(grounded){
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

        if(wallrunning){
            state = MovementState.wallrunning;
            moveSpeed = wallRunSpeed;
        }
        if(grounded){
            if(Input.GetKey(sprintKey)){
                state = MovementState.sprinting;
                moveSpeed = sprintSpeed;
            }
            else{
                state = MovementState.walking;
                moveSpeed = walkSpeed;
            }
        }
        else{
            state = MovementState.Airborne;
        }
    }

    private void MovePlayer(){
        // calculate movement direction and add force to player rigid body
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControler(){
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
    }
}
