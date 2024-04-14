using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip alienAttackSound;
    public AudioClip alienAggroSound;
    public AudioClip playerDeathSound;
    public AudioClip background;
    public AudioClip[] metalFootsteps;
    public AudioClip[] outsideFootsteps;
    public AudioClip[] jump;
    public AudioClip itemEquip;


    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
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
