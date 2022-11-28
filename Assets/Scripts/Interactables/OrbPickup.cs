using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrbPickup : MonoBehaviour
{
    
    public UnityEvent<int> _orbPickUpEvent;
    [SerializeField] int healAmount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _orbPickUpEvent?.Invoke(healAmount);
            Destroy(gameObject);
        }
    }
}
