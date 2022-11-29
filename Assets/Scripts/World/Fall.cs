using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fall : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private int _fallDamage;
    public UnityEvent<int> FallEvent;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TPlayer());
            FallEvent?.Invoke(_fallDamage);
            //jugador.transform.position = CheckPoint.ReachedPoint;
            //Physics.SyncTransforms();
        }
    }

    public void CheckFall(){
        Debug.Log("Fall");
    }
    
    IEnumerator TPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        _player.transform.position = CheckPoint.s_reachedPoint;
        Physics.SyncTransforms();
    }


}
