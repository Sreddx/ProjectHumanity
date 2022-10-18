using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [SerializeField] private Transform _player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TPlayer());
            //jugador.transform.position = CheckPoint.ReachedPoint;
            //Physics.SyncTransforms();
        }
    }

    IEnumerator TPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        _player.transform.position = CheckPoint.s_reachedPoint;
        Physics.SyncTransforms();
    }

}
