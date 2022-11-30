using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitBoxTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent OnHit;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Enemy"){
            OnHit?.Invoke();
        }
    }
}
