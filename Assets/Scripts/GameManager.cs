using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }
    
    
    //Check that there is no other instance of this class
    void Awake() {
        if (gameManager != null  && gameManager != this) {
            Destroy(gameObject);
        } else {
            gameManager = this;
        }
    }

    //Coroutine to reload scene after 3 seconds of player death
    IEnumerator ReloadScene() {
        yield return new WaitForSeconds(1.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadNextLevel() {
        yield return new WaitForSeconds(5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
    }

    public void ElevatorNextLevel() {
        StartCoroutine(LoadNextLevel());
    }

    //Method to relaod scene
    public void ReloadSceneOnPlayerDeath() {
        StartCoroutine(ReloadScene()); 
    }

}
