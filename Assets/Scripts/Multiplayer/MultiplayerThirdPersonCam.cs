using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Fusion;
using Fusion.Sockets;

public class MultiplayerThirdPersonCam : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] Transform _orientation;
    [SerializeField] Transform _player;
    [SerializeField] Transform _playerObj;
    [SerializeField] Rigidbody rb;
    [SerializeField] CinemachineFreeLook _freeLookCam;

    [SerializeField] private float _rotationSpeed;
    private bool _playerFound = false;

    private void Awake() {
        

    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public override void FixedUpdateNetwork() 
    {
        if(_playerFound){
            if(GetInput(out NetworkInputData input)){
                // rotate _orientation by checking view direction of camera and rotating orientation of player towards that direction
                Vector3 viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
                _orientation.forward = viewDir.normalized;

                // rotate _player object to face _orientation by getting inputs and calculating input dir
                // float horizontalInput = input.HorizontalInput;
                // float verticalInput = input.VerticalInput;
                Vector3 inputDir = _orientation.forward * input.VerticalInput + _orientation.right * input.HorizontalInput;;

                if (inputDir != Vector3.zero)
                {
                    // rotate _player object smoothly based on input and rotation speed
                    _playerObj.forward = Vector3.Slerp(_playerObj.forward, inputDir.normalized, _rotationSpeed * Time.deltaTime);
                }
            }
        }
    }

    private void SetCinemachineReferences(){
        _freeLookCam.Follow = _player;
        _freeLookCam.LookAt = _player;
        //Make middle rig look at player
        _freeLookCam.GetRig(1).LookAt = _player;
        _playerFound = true;

    }

    public void LookForPlayer(){
        //Get Player references
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        //get child of _player that is the player object
        _playerObj = _player.GetChild(0);
        //get child of _player that is orientation
        _orientation = _player.GetChild(1);
        rb = _player.GetComponent<Rigidbody>();
        SetCinemachineReferences();
    }
}
