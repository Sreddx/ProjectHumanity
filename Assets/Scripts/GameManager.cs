using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }
    
    //public UnitHealth _playerHealth = new UnitHealth(100,100); //CurrentHealth and MaxHealth in constructor
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemy;
    
    //Check that there is no other instance of this class
    void Awake() {
        if (gameManager != null  && gameManager != this) {
            Destroy(gameObject);
        } else {
            gameManager = this;
        }
    }

    void Update()
    {
        
    }

    //Method to relaod scene
    public void ReloadScene() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

}
