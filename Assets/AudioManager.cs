using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour

{
    public static AudioManager Instance;
    // Start is called before the first frame update
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if(s==null)
        {
            Debug.Log("Ses bulunamadi ");
        }

        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();

        }
    }

    


}
