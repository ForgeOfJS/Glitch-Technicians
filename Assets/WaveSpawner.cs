using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public int enemyCount;
    public int enemyAliveCount;
    public Wave wave;
    bool spawned = false;

    public void StartWave()
    {
        if (spawned && enemyAliveCount > 0) 
        {
            return;
        }
        wave.agents = new GameObject[enemyCount];
        enemyAliveCount = enemyCount;
        wave.hasAggroed = false;
        wave.isChasing = false;
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(Resources.Load<GameObject>("Enemy"));
            enemy.transform.position = new Vector3(transform.position.x + Random.Range(-10f, 10f), transform.position.y, transform.position.z + Random.Range(-10f, 10f));
            float randomScale = Random.Range(.3f, 1f);
            enemy.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            enemy.GetComponent<EnemyHealth>().maxHealth = 100f * randomScale;
            enemy.GetComponent<EnemyHealth>().health = 100f * randomScale;
            enemy.GetComponent<EnemyHealth>().waveSpawner = this;
            wave.agents[i] = enemy;
            enemy.transform.GetChild(1).GetComponent<AgentDetection>().wave = wave;
        }

        spawned = true;
    }

    public void EnemyDeath()
    {
        enemyAliveCount -= 1;
    }

    void Update()
    {
        if (!spawned) return;

        if (enemyAliveCount == 0) StartWave();
    }
}
