using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth 
{
    //Fields
    int _currentHealth;
    int _currentMaxHealth;

    //Properties
    public int Health
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

    public int MaxHealth
    {
        get { return _currentMaxHealth; }
        set { _currentMaxHealth = value; }
    }

    //Constructor
    public UnitHealth(int health, int maxHealth)
    {
        _currentHealth = health;
        _currentMaxHealth = maxHealth;
    }

    //Methods
    public void DmgUnit(int damageAmount)
    {
        if(_currentHealth > 0)
        {
            _currentHealth -= damageAmount;
        }
        //Reportar que le hicieron daño a la unidad con evento
        
    }

    public void Heal(int healAmount)
    {
        if(_currentHealth > 0 && _currentHealth < _currentMaxHealth)
        {
            _currentHealth += healAmount;   
        }
        if(_currentHealth>_currentMaxHealth){
            _currentHealth = _currentMaxHealth;
        }
        
    }



}
