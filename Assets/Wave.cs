using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public GameObject[] agents;
    public bool isChasing = false;
    public bool hasAggroed = false;
    public void StartWaveChase(GameObject player)
    {
        
        if (!isChasing) isChasing = true;
        foreach (GameObject agent in agents)
        {
            agent.GetComponent<Agent>().isChasing = true;
            agent.GetComponent<Agent>().player = player;
            if (!hasAggroed) agent.transform.GetComponent<AudioSource>().PlayOneShot(AudioManager.Instance.alienAggroSound);
            hasAggroed = true;
        }
    }
}
