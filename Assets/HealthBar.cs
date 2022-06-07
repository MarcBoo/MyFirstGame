using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    PlayerHealth playerHealth;

    // Start connects with the Player's Health
    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Updates the Healthbar every Frame
    private void Update()
    {
        healthBar.value = playerHealth.health;
    }
}
