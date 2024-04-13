using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public UnityEvent deathEvent;

    public float health;
    bool isDead = false;

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
            transform.GetComponent<Animator>().SetTrigger("Die");
            deathEvent.Invoke();
            isDead = true;
            StartCoroutine(DelayDelete());
        }
    }

    IEnumerator DelayDelete()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this);
    }
}
