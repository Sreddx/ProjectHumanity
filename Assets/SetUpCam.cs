using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class SetUpCam : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;

    private void Start()
    {
        _camera = GameObject.Find("CM vcam1");
        _virtualCamera = _camera.GetComponent<CinemachineVirtualCamera>();
        _virtualCamera.Follow = transform;
        _virutalCamera.LookAt = transform;
    }
}
