using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using TMPro;

public class PromptUI : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI promptText;
    private bool Interactuable;
    private bool DoorInRange;
    private bool KeyInRange;
    
    private void Start()
    {
        promptText.text = " ";
    }

    private void Update()
    {
        if( Interactuable == true)
        {
            if (DoorInRange)
            {
                promptText.text = "Press E to Interact";
            }

            if (KeyInRange)
            {
                promptText.text = "Press E to Pick Up";
                if (Input.GetKeyDown(KeyCode.E)) 
                {
                    Interactuable = false;
                }
            } 
        }
        else if (Interactuable == false) 
        {
            promptText.text = " ";
            DoorInRange = false;
            KeyInRange = false;
        }
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Key")
        {
            KeyInRange = true;
            Interactuable = true;
        }
         if (collider.gameObject.tag == "Puerta")
        {
            DoorInRange = true;
            Interactuable = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
          if (collider.gameObject.tag == "Key")
        {
            Interactuable = false;
        }
          if (collider.gameObject.tag == "Puerta")
        {
            Interactuable = false;
        }
    }

       
}
