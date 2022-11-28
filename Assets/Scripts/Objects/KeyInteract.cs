using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteract : MonoBehaviour
{
    [SerializeField] GameObject llaveUsada;

    private bool playerInRange;
    

    private void Awake()
    {
      
    }

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Inventory.keys[llaveUsada.GetComponent<Key>().index] = true;
              
                llaveUsada.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    



   
}
