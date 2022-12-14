using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class PlayerStatus : MonoBehaviour
{

    [SerializeField] HealthBarUI healthBar;
    public UnitHealth _playerHealth = new UnitHealth(200,200); //CurrentHealth and MaxHealth in constructor
    [SerializeField] private UnityEvent OnPlayerDeath;
    [SerializeField] private UnityEvent<int> OnPlayerDamage;
    [SerializeField] private UnityEvent<int> OnPlayerHeal;
    void Start()
    {
        // if (m_onPlayerDamage == null)
        //     m_onPlayerDamage = new OnPlayerDamage();

        healthBar.SetMaxHealth(_playerHealth.MaxHealth);
        // m_onPlayerDamage.AddListener(healthBar.SetHealth);
    }

    
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.H)){ //Testing only
        //     PlayerHeal(30);
        // }
    }
    

    private void OnTriggerEnter(Collider other){
        
        if(other.tag == "Enemy"){
            PlayerTakeDamage(25);
        }

    }

    public void PlayerTakeDamage(int damage) {
        _playerHealth.DmgUnit(damage);
        //healthBar.SetHealth(_playerHealth.Health);
        //Invoke event and pass damage value
        OnPlayerDamage?.Invoke(_playerHealth.Health);

        

        if(_playerHealth.Health <= 0){
            OnPlayerDeath?.Invoke();
        }

    }

    public void PlayerHeal(int heal){
        _playerHealth.Heal(heal);
        OnPlayerHeal?.Invoke(_playerHealth.Health);
    }


}
