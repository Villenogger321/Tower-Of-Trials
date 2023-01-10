using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    bool screenShake = true;

      public void SetVolume(float masterVolume)
    {
        audioMixer.SetFloat("masterVolume", masterVolume);
    }

    public void SetMusic(float musicVolume)
    {
        audioMixer.SetFloat("musicVolume", musicVolume);
    }

    public void SetSFX(float sfxVolume)
    {
        audioMixer.SetFloat("sfxVolume", sfxVolume);
    }

    public void ScreenShake()
    {
        if (screenShake)
        {
            screenShake = false;
            Debug.Log("screenshake off");
        }
        else if (!screenShake)
        {
            screenShake = true;
            Debug.Log("screenshake on");
        }
    }

 

}
