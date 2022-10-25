using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMaster : MonoBehaviour
{
    
    // General state machine variables
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _animator;
    private Ray ray;
    private RaycastHit hit;
    [SerializeField] private float _maxDistanceToCheck = 6.0f;
    private float _currentDistance;
    private Vector3 _checkDirection;

    // Patrol state variables
    public Transform _pointA;
    public Transform _pointB;
    [SerializeField] private UnityEngine.AI.NavMeshAgent _navMeshAgent;
    
    private int _currentTarget;
    private float _distanceFromTarget;
    private Transform[] _waypoints = null;
    

    private void Awake() {
        _pointA = GameObject.Find("p1").transform;
        _pointB = GameObject.Find("p2").transform;
        _navMeshAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        _waypoints = new Transform[2] {
            _pointA,
            _pointB
        };
        _currentTarget = 0;
        _navMeshAgent.SetDestination(_waypoints[_currentTarget].position);
    }

    private void FixedUpdate() {
        //First we check distance from the player 
        _currentDistance = Vector3.Distance(_player.transform.position, transform.position);
        _animator.SetFloat("distanceFromPlayer", _currentDistance);
        

        

        //Lastly, we get the distance to the next waypoint target
        _distanceFromTarget = Vector3.Distance(_waypoints[_currentTarget].position, transform.position);
        _animator.SetFloat("distanceFromWaypoint", _distanceFromTarget);
    }

    public void SetNextPoint() {
        switch (_currentTarget) {
            case 0:
                _currentTarget = 1;
                break;
            case 1:
                _currentTarget = 0;
                break;
        }
        _navMeshAgent.SetDestination(_waypoints[_currentTarget].position);
    }


    public void ChasePlayer() {
        _navMeshAgent.SetDestination(_player.transform.position);
        Debug.Log("Chasing player");
    }

}
