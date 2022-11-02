using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [System.Serializable]
// public class OnPlayerDamage : UnityEvent<int>
// {
// }


public class PlayerStatus : MonoBehaviour
{

    [SerializeField] HealthBarUI healthBar;
    public UnitHealth _playerHealth = new UnitHealth(100,100); //CurrentHealth and MaxHealth in constructor
    [SerializeField] private UnityEvent OnPlayerDeath;
    [SerializeField] private UnityEvent OnPlayerDamage;
    void Start()
    {
        // if (m_onPlayerDamage == null)
        //     m_onPlayerDamage = new OnPlayerDamage();

        healthBar.SetMaxHealth(_playerHealth.MaxHealth);
        // m_onPlayerDamage.AddListener(healthBar.SetHealth);
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)){ //Testing only
            PlayerHeal(30);
        }
    }
    

    private void OnTriggerEnter(Collider other){
        
        if(other.tag == "Enemy"){
            PlayerTakeDamage(10);
        }

    }

    private void PlayerTakeDamage(int damage) {
        _playerHealth.DmgUnit(damage);
        //healthBar.SetHealth(_playerHealth.Health);
        //Invoke event and pass damage value
        OnPlayerDamage?.Invoke();

        

        if(_playerHealth.Health <= 0){
            OnPlayerDeath?.Invoke();
        }

    }

    private void PlayerHeal(int healing) {
        _playerHealth.Heal(healing);
        healthBar.SetHealth();
    }
}
