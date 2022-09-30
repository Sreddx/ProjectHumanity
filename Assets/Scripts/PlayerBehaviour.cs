using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        //If I press f, make the player take damage
        // if(Input.GetKeyDown(KeyCode.F)){
        //     playerTakeDamage(50);
        //     Debug.Log(GameManager.gameManager._playerHealth.Health);
        // }
        if(Input.GetKeyDown(KeyCode.H)){
            PlayerHeal(30);
            Debug.Log(GameManager.gameManager._playerHealth.Health);
        }
    }
    
    


    private void OnTriggerEnter(Collider other){
        
        if(other.tag == "Enemy"){
            PlayerTakeDamage(10);
            Debug.Log(GameManager.gameManager._playerHealth.Health);
        }

    }

    private void PlayerTakeDamage(int damage) {
        GameManager.gameManager._playerHealth.DmgUnit(damage);
    }

    private void PlayerHeal(int healing) {
        GameManager.gameManager._playerHealth.Heal(healing);
    }
}
