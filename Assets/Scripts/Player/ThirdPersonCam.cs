using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform _orientation;
    [SerializeField] Transform _player;
    [SerializeField] Transform _playerObj;
    [SerializeField] Rigidbody rb;

    [SerializeField] private float _rotationSpeed;

    private void Awake() {
        //Get Player references
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        //get child of _player that is the player object
        _playerObj = _player.GetChild(0);
        //get child of _player that is orientation
        _orientation = _player.GetChild(1);
        rb = _player.GetComponent<Rigidbody>();

    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() 
    {
        // rotate _orientation by checking view direction of camera and rotating orientation of player towards that direction
        Vector3 viewDir = _player.position - new Vector3(transform.position.x, _player.position.y, transform.position.z);
        _orientation.forward = viewDir.normalized;

        // rotate _player object to face _orientation by getting inputs and calculating input dir
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = _orientation.forward * verticalInput + _orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
        {
            // rotate _player object smoothly based on input and rotation speed
            _playerObj.forward = Vector3.Slerp(_playerObj.forward, inputDir.normalized, _rotationSpeed * Time.deltaTime);
        }
    }
}
