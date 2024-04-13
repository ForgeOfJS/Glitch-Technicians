using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public UnityEvent deathEvent;
    public float maxHealth = 100f;
    [SerializeField]
    float playerHealth;
    [HideInInspector]
    public bool isDead = false;

    void Start()
    {
        playerHealth = maxHealth;
    }



    public void DamagePlayer(float damage)
    {

        playerHealth -= damage;
        if (playerHealth <= 0f && !isDead)
        {
            playerHealth = 0f;
            //player dead
            print("dead");
            PlayerDeath();
        }
    }

    public void HealPlayer(float damage)
    {
        playerHealth += damage;
        if (playerHealth > maxHealth) playerHealth = maxHealth;
    }

    public void PlayerDeath()
    {
        isDead = true;
        deathEvent.Invoke();
    }
}
