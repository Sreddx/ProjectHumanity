using UnityEngine;

public class Perspective : Sense
{
    [SerializeField] private int fieldOfView = 45;
    [SerializeField] private int viewDistance = 100;
    [SerializeField] private Animator _animator;

    private Transform _playerTransform;
    private Vector3 rayDirection;

    protected override void Initialize() 
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void UpdateSense() 
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= detectionRate) 
        {
            DetectAspect();
        }
	}

    //Detect perspective field of view for the AI Enemy
    void DetectAspect()
    {
        RaycastHit hit;
        rayDirection = _playerTransform.position - transform.position; //Get direction of player

        if ((Vector3.Angle(rayDirection, transform.forward)) < fieldOfView) //Check if player is within field of view
        {
            
            if (Physics.Raycast(transform.position, rayDirection, out hit, viewDistance)) 
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();
                if (aspect != null)
                {
                    //Check the aspect
                    if (aspect.aspectType != aspectName)
                    {
                        Debug.Log("Player Detected");
                    }
                }
            }
        }
    }

    /// <summary>
    /// Show Debug Grids and obstacles inside the editor
    /// </summary>
    void OnDrawGizmos()
    {
        if (_playerTransform == null) 
        {
            return;
        }

        Debug.DrawLine(transform.position, _playerTransform.position, Color.red);

        Vector3 frontRayPoint = transform.position + (transform.forward * viewDistance);

        //Approximate perspective visualization
        Vector3 leftRayPoint = frontRayPoint;
        leftRayPoint.x += fieldOfView * 0.5f;

        Vector3 rightRayPoint = frontRayPoint;
        rightRayPoint.x -= fieldOfView * 0.5f;

        Debug.DrawLine(transform.position, frontRayPoint, Color.green);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);
    }
}
