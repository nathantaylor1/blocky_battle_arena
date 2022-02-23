using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STATIC_AudioManager : MonoBehaviour
{
    public static STATIC_AudioManager instance;
    public float volume = 0.6f;
    private AudioSource musicAudioSrc;
    AudioClip komiku_school, komiku_mall, komiku_battleofpogs;

    private void Awake()
    {
        if (instance != null)
        {
            print("Already an STATIC_AudioManager instance");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        
        musicAudioSrc = gameObject.AddComponent<AudioSource>();
        musicAudioSrc.volume = volume;
        musicAudioSrc.loop = true;
        
        komiku_school       = Resources.Load<AudioClip>("Audio/komiku_school");
        komiku_mall         = Resources.Load<AudioClip>("Audio/komiku_mall");
        // Assets/Resources/Audio/komiku_battleofpogs.mp3
        komiku_battleofpogs = Resources.Load<AudioClip>("Audio/komiku_battleofpogs");
    }

    public void PlayMainMenuAudio()
    {
        print("playing komiku_school");
        musicAudioSrc.clip = komiku_school;
        musicAudioSrc.Play();
    }

    public void PlayGameAudio()
    {
        print("playing komiku_mall");
        musicAudioSrc.clip = komiku_mall;
        musicAudioSrc.Play();
    }

    public void PlayLateGameAudio()
    {
        print("playing komiku_battleofpogs");
        musicAudioSrc.clip = komiku_battleofpogs;
        musicAudioSrc.Play();
    }
}
