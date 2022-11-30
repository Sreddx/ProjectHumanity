using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    private bool cooldown = false;
    [SerializeField] GameObject door;
    private bool playerInRange;

    private void Update()
    {
        if (playerInRange)
        {
            Door doorOpener = door.GetComponent<Door>();
            if (doorOpener == null)
                return;
            if (Inventory.keys[doorOpener.index] == true)
            {
                if( cooldown == false)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        doorOpener.ChangeDoorState();
                         Invoke("ResetCooldown",2.0f);
                        cooldown = true;
                    }
                }
                
            }
        }

    }

    void ResetCooldown()
    {
        cooldown = false;
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
