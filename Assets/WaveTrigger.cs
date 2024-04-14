using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    public WaveSpawner waveSpawner;
    public bool multiple;
    void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.tag == "Player")
        {
            print("!");
            if (multiple)
            {
                foreach (Transform transform in this.transform.parent.GetComponentsInChildren<Transform>())
                {
                    if (transform.GetComponent<WaveSpawner>())
                    {
                        transform.GetComponent<WaveSpawner>().StartWave();
                    }
                }
                return;
            }
            waveSpawner.StartWave();
        }
    }
}
