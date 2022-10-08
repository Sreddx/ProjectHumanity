using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wall Running")]
    [SerializeField] private LayerMask _wallLayer;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _wallRunForce;
    [SerializeField] private float _maxWallRunTime;
    private float wallRunTimer;
    //Jump vars
    
    [SerializeField] private float _wallJumpUpForce;
    [SerializeField] private float _wallJumpSideForce;
    

    [Header("Input")]
    private float _horizontalInput;
    private float _verticalInput;
    private  KeyCode _jumpKey = KeyCode.Space;

    [Header("Detection")]
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private float _minJumpHeight;

    private RaycastHit _leftWallHit;
    private RaycastHit _rightWallHit;
    private bool _wallLeft;
    private bool _wallRight;

    [Header("Exiting")]
    private bool _exitingWall;
    public float _exitWallTime;
    private float _exitWallTimer;

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
        //Debug.Log(_exitingWall);

    }

    private void FixedUpdate(){
        if(pm.wallrunning){
            WallRunningMovement();
        }
    }

    private void CheckForWall(){
        //Check for wall on the left
        _wallLeft = Physics.Raycast(transform.position, -orientation.right, out _leftWallHit, _wallCheckDistance, _wallLayer);

        //Check for wall on the right
        _wallRight = Physics.Raycast(transform.position, orientation.right, out _rightWallHit, _wallCheckDistance, _wallLayer);
            
    }

    private bool AboveGround(){
        return !Physics.Raycast(transform.position, Vector3.down, _minJumpHeight, _groundLayer);
    }

    private void StateMachine(){
        //Getting inputs
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        //State 1 - Wall running
        if((_wallLeft || _wallRight) && _verticalInput > 0 && AboveGround() && !_exitingWall){
            if(!pm.wallrunning){
                StartWallRun();
            }
            
            //Wall Run timer
            if (wallRunTimer > 0){
                wallRunTimer -= Time.deltaTime;
            }

            if (wallRunTimer <= 0 && pm.wallrunning){
                _exitingWall = true;
                _exitWallTimer = _exitWallTime;
            }
            //wall jump
            if(Input.GetKeyDown(_jumpKey)){
                WallJump();
            }
        }
        //State 2 - Exiting wall
        else if(_exitingWall){
            if(pm.wallrunning)
                StopWallRun();
        
            if (_exitWallTimer > 0 )
                _exitWallTimer -= Time.deltaTime;

            if (_exitWallTimer <= 0)
                _exitingWall = false;
            

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
        wallRunTimer = _maxWallRunTime;
    }

    private void WallRunningMovement(){
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        
        Vector3 wallNormal = _wallRight ? _rightWallHit.normal : _leftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, Vector3.up);

        if((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude){
            wallForward = -wallForward;
        }

        //Add force to the player
        rb.AddForce(wallForward * _wallRunForce, ForceMode.Force);

        //Make player stick to wall if they are not moving away from it
        if(!(_wallLeft && _horizontalInput >0) && !(_wallRight && _horizontalInput <0)){
            rb.AddForce(-wallNormal * 90, ForceMode.Force);
        }

    }

    private void StopWallRun(){
        pm.wallrunning = false;
    }

    private void WallJump(){
        // enter exiting wall state
        _exitingWall = true;
        _exitWallTimer = _exitWallTime;

        //Calculate wall jump direction and force
        Vector3 wallNormal = _wallRight ? _rightWallHit.normal : _leftWallHit.normal;

        Vector3 forceToApply = transform.up * _wallJumpUpForce + wallNormal * _wallJumpSideForce;

        //Add force to the player
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(forceToApply, ForceMode.Impulse);
        
    }

     private void OnDrawGizmos() {
        // draw ground check ray
        Gizmos.color = Color.red;
        //draw raycast for wall check
        Gizmos.DrawRay(transform.position, -orientation.right * _wallCheckDistance);
    }

}
