using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.tag == "Player")
        {
            print("!");
            waveSpawner.StartWave();
        }
    }
}
