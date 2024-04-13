using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip alienAttackSound;
    public AudioClip alienAggroSound;
    public AudioClip playerDeathSound;
    public static AudioManager Instance { 
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(AudioManager)) as AudioManager;
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static AudioManager instance;
}
