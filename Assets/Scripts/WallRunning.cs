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
    //Jump vars
    public float wallJumpUpForce;
    public float wallJumpSideForce;
    

    [Header("Input")]
    private float horizontalInput;
    private float verticalInput;
    public  KeyCode jumpKey = KeyCode.Space;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;

    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;

    [Header("Exiting")]
    private bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

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
        //Debug.Log(exitingWall);

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
        if((wallLeft || wallRight) && verticalInput > 0 && AboveGround() && !exitingWall){
            if(!pm.wallrunning){
                StartWallRun();
            }
            
            //Wall Run timer
            if (wallRunTimer > 0){
                wallRunTimer -= Time.deltaTime;
            }

            if (wallRunTimer <= 0 && pm.wallrunning){
                exitingWall = true;
                exitWallTimer = exitWallTime;
            }
            //wall jump
            if(Input.GetKeyDown(jumpKey)){
                WallJump();
            }
        }
        //State 2 - Exiting wall
        else if(exitingWall){
            if(pm.wallrunning)
                StopWallRun();
        
            if (exitWallTimer > 0 )
                exitWallTimer -= Time.deltaTime;

            if (exitWallTimer <= 0)
                exitingWall = false;
            

        }
        //State 3 - None
        else
        {
            if(pm.wallrunning)
                StopWallRun();
        }
    }

    private void StartWallRun(){
        pm.wallrunning = true;
        wallRunTimer = maxWallRunTime;
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
            rb.AddForce(-wallNormal * 90, ForceMode.Force);
        }

    }

    private void StopWallRun(){
        pm.wallrunning = false;
    }

    private void WallJump(){
        // enter exiting wall state
        exitingWall = true;
        exitWallTimer = exitWallTime;

        //Calculate wall jump direction and force
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        //Add force to the player
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
        
    }

     private void OnDrawGizmos() {
        // draw ground check ray
        Gizmos.color = Color.red;
        //draw raycast for wall check
        Gizmos.DrawRay(transform.position, -orientation.right * wallCheckDistance);
    }

}
