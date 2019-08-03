using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public Sound[] music;
    private int song;

    private AudioSource musicAudioSource;

    // Audio
    [HideInInspector]
    public float musicVolume = 1;
    public float SFXVolume = 1;




    // Start is called before the first frame update
    void Awake()
    {
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if (s.source == null)
        {
            return;
        } else
        {
            s.source.volume = SFXVolume;
            s.source.Play();
        }
    }


    public void MusicSlider(float sliderValue)
    {
        musicVolume = sliderValue;
        
        
    }
 

    public void SFXSlider(float sliderValue)
    {
        SFXVolume = sliderValue;
    }

}
