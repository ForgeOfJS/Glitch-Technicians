using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentDetection : MonoBehaviour
{

    public Wave wave;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            wave.StartWaveChase(other.gameObject);
        }
    }

}
