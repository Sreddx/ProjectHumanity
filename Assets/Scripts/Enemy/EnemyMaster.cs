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
    private float _currentDistance;
    private Vector3 _checkDirection;

    // Patrol state variables
    
    [SerializeField] private UnityEngine.AI.NavMeshAgent _navMeshAgent;
    
    private int _currentTarget;
    private float _distanceFromTarget;
    [SerializeField] private Transform[] _waypoints = null;
    

    private void Awake() {
        //_navMeshAgent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        
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
