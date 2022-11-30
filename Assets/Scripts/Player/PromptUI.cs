using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using TMPro;

public class PromptUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI promptText;
     private bool DoorInRange;
     private bool KeyInRange;
    
    private void Start()
    {
        promptText.text = " ";
    }

    private void Update()
    {
        if (DoorInRange)
        {
            promptText.text = "Press E to Interact";
        }
        else
        {
            promptText.text = " ";
        }

        if (KeyInRange)
        {
            promptText.text = "Press E to Pick Up";
        }
        else
        {
            promptText.text = " ";
        }
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Door")
        {
            DoorInRange = true;
        }
        if (collider.gameObject.tag == "Key")
        {
            KeyInRange = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Door")
        {
            DoorInRange = false;
        }
          if (collider.gameObject.tag == "Key")
        {
            KeyInRange = false;
        }
    }

    
}
