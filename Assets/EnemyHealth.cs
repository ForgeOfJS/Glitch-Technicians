using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public UnityEvent deathEvent;

    public float health;
    public Animator animator;
    public bool isDead = false;
    public WaveSpawner waveSpawner;

    void Start()
    {
        health = maxHealth;
    }

    public void DamageEnemy(float damage)
    {
        health -= damage;
        if (health <= 0 && !isDead)
        {
            health = 0;
            animator.SetTrigger("Die");
            deathEvent.Invoke();
            waveSpawner.EnemyDeath();
            isDead = true;
            Destroy(this.gameObject, 5f);
        }
    }
}
