using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }
    
    public UnitHealth _playerHealth = new UnitHealth(100,100); //CurrentHealth and MaxHealth in constructor

    //Check that there is no other instance of this class
    void Awake() {
        if (gameManager != null  && gameManager != this) {
            Destroy(gameObject);
        } else {
            gameManager = this;
        }
    }
}
