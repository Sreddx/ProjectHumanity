using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMaster : MonoBehaviour
{
    public GameObject player;
    private float distance;
    public float detectionDistance;
    public bool isAngered;

    public NavMeshAgent enemyAgent;


    private void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        
    }

    private void Update()
    {
        // if (isAngered)
        // {
        //     enemyAgent.SetDestination(Player.transform.position);
        // }
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        checkPlayerDistance(distance);
        if(isAngered){
            enemyAgent.isStopped = false;
            enemyAgent.SetDestination(player.transform.position);
        }else{
            enemyAgent.isStopped = true;
        }
    }

    private void checkPlayerDistance(float distance)
    {
        if (distance <= detectionDistance)
        {
            isAngered = true;
        }
        else
        {
            isAngered = false;
        }
    }

}
