using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This is so that it should find the Text component
using UnityEngine.Events; // This is so that you can extend the pointer handlers
using UnityEngine.EventSystems; // This is so that you can extend the pointer handlers

public class HealthBarUI : MonoBehaviour
{

    public Slider slider;
	public Gradient gradient;
	public Image fill;
	public GameObject player;
	private PlayerStatus _playerStatusHealth;

	private void Start() {
		player = GameObject.Find("Player");
		_playerStatusHealth = player.GetComponent<PlayerStatus>();
	}

	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;

		fill.color = gradient.Evaluate(1f);
	}

    public void SetHealth()
	{	
		slider.value = _playerStatusHealth._playerHealth.Health;
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
