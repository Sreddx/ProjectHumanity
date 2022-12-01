using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteract : MonoBehaviour
{
    [SerializeField] GameObject llaveUsada;

    private bool playerInRange;

    [SerializeField] AudioSource _playerAudioSource;
    [SerializeField] AudioClip[] _keyAudioClips;
    

    private void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Inventory.keys[llaveUsada.GetComponent<Key>().index] = true;
                _playerAudioSource.PlayOneShot(_keyAudioClips[0]);
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
