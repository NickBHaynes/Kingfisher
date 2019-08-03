using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    public AudioSource theAS;
    // Start is called before the first frame update
    void Start()
    {
        theAS.volume = FindObjectOfType<AudioManager>().musicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuMusicSlider(float musicVolume)
    {
        theAS.volume = musicVolume;
    }
}
