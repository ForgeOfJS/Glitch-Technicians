using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public PlayerHealth playerHealth;

    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // Calculate the current health percentage
        float currentHealth = playerHealth.GetHealth;
        float currentHealthPercentage = currentHealth / playerHealth.maxHealth;

        // Update health bar fill amount based on player's current health percentage
        healthBar.fillAmount = currentHealthPercentage;
    }
}
