using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    [SerializeField] private Transform jugador;


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
        yield return new WaitForSeconds(1);
        jugador.transform.position = CheckPoint.ReachedPoint;
        Physics.SyncTransforms();
    }

}
