using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public Agent[] agents;
    public void StartWaveChase(GameObject player)
    {
        foreach (Agent agent in agents)
        {
            agent.isChasing = true;
            agent.player = player;
            agent.transform.GetComponent<AudioSource>().PlayOneShot(AudioManager.Instance.alienAggroSound);
        }
    }
}
