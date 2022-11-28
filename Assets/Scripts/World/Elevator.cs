using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float _elevatorSpeed;
    private bool _elevatorPressed; 
    public UnityEvent _elevatorOn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _elevatorPressed = true;
            _elevatorOn?.Invoke();
        }
    }

    private void Update() {
        if (_elevatorPressed) {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), _elevatorSpeed * Time.deltaTime);
        }
    }

}
