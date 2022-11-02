using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    [SerializeField] Vida healthBar;

    private int _maxHealth;
    private int _currentHealth;
    
    void Start()
    {
        _maxHealth = GameManager.gameManager._playerHealth.Health;
        _currentHealth = _maxHealth;
        healthBar.SetMaxHealth(_maxHealth);
    }

    
    void Update()
    {
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
        _currentHealth -= damage;
        healthBar.SetHealth(_currentHealth);

    }

    private void PlayerHeal(int healing) {
        GameManager.gameManager._playerHealth.Heal(healing);
        _currentHealth += healing;
        healthBar.SetHealth(_currentHealth);


    }
}
