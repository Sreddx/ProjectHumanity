using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wall Running")]
    public LayerMask wallLayer;
    public LayerMask groundLayer;
    public float wallRunForce;
    public float maxWallRunTime;
    private float wallRunTimer;

    [Header("Input")]
    private float horizontalInput;
    private float verticalInput;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;

    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;

    [Header("References")]
    public Transform orientation;
    private PlayerMovement pm;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        CheckForWall();
        StateMachine();
    }

    private void FixedUpdate(){
        if(pm.wallrunning){
            WallRunningMovement();
        }
    }

    private void CheckForWall(){
        //Check for wall on the left
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance, wallLayer);

        //Check for wall on the right
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance, wallLayer);
            
    }

    private bool AboveGround(){
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, groundLayer);
    }

    private void StateMachine(){
        //Getting inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //State 1 - Wall running
        if((wallLeft || wallRight) && verticalInput > 0 && AboveGround()){
            if(!pm.wallrunning){
                StartWallRun();
            }
        }
        //State 3 - None
        else
        {
            if(pm.wallrunning){
                StopWallRun();
            }
        }
    }

    private void StartWallRun(){
        pm.wallrunning = true;

    }

    private void WallRunningMovement(){
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, Vector3.up);

        if((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude){
            wallForward = -wallForward;
        }

        //Add force to the player
        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        //Make player stick to wall
        if(!(wallLeft && horizontalInput >0) && !(wallRight && horizontalInput <0)){
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }

    }

    private void StopWallRun(){
        pm.wallrunning = false;
        
    }

}
