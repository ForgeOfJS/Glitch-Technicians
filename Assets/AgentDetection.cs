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
            print(other.name);
            wave.StartWaveChase(other.gameObject);
        }
    }

}
