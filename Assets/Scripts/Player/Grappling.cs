using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour
{
     [Header("References")]
    private PlayerMovement pm;
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

    [Header("Grappling")]
    [SerializeField] private float _maxGrappleDistance;
    [SerializeField] private float _grappleDelayTime;
    [SerializeField] private float _overshootYAxis;

    private Vector3 _grapplePoint;

    [Header("Cooldown")]
    [SerializeField] private float _grapplingCd;
    private float _grapplingCdTimer;

    [Header("Input")]
    public KeyCode grappleKey = KeyCode.Mouse1;

    private bool _grappling;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(grappleKey)) StartGrapple();

        if(_grapplingCdTimer > 0){
            _grapplingCdTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate(){
        if (_grappling){
            lr.SetPosition(0,gunTip.position);
        }
    }

    private void StartGrapple()
    {
        if (_grapplingCdTimer > 0) return;

        _grappling = true;

        pm.freeze = true;

        RaycastHit hit;
        
        if(Physics.Raycast(cam.position, cam.forward, out hit, _maxGrappleDistance, whatIsGrappleable))
        {
            _grapplePoint = hit.point;

            Invoke(nameof(ExecuteGrapple), _grappleDelayTime);
        }
        else
        {
            _grapplePoint = cam.position + cam.forward * _maxGrappleDistance;

            Invoke(nameof(StopGrapple), _grappleDelayTime);
        }

        lr.enabled = true;
        lr.SetPosition(1, _grapplePoint);
    }

    private void ExecuteGrapple()
    {
        pm.freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float _grapplePointRelativeYPos = _grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = _grapplePointRelativeYPos + _overshootYAxis;

        if (_grapplePointRelativeYPos < 0) highestPointOnArc = _overshootYAxis;

        pm.JumpToPosition(_grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        pm.freeze = false;

        _grappling = false;

        _grapplingCdTimer = _grapplingCd;

        lr.enabled = false;
    }


    
}
