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

    private void Start() {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        _playerObj =  _player.GetChild(0);
        _orientation = _player.GetChild(1);
    }

    private void Update() 
    {
        // rotate _orientation
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
