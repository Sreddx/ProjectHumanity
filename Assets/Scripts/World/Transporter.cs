using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transporter : MonoBehaviour
{
     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(cargarEscena());
            
        }
    }

    IEnumerator cargarEscena()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
     
}
