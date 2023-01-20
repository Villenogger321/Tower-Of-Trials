using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Timeline;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    bool screenShake = true;

    #region FMOD

    private FMOD.Studio.VCA masterVCA;
    private FMOD.Studio.VCA musicVCA;
    private FMOD.Studio.VCA sfxVCA;

    void Start()
    {
        masterVCA = FMODUnity.RuntimeManager.GetVCA("vca:/ Master");
        musicVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Music");
        sfxVCA = FMODUnity.RuntimeManager.GetVCA("vca:/SFX");
    }

    #endregion


    public void SetVolume(float masterVolume)
    {
        audioMixer.SetFloat("masterVolume", masterVolume);
        masterVCA.setVolume(masterVolume); //FMOD master VCA
    }

    public void SetMusic(float musicVolume)
    {
        audioMixer.SetFloat("musicVolume", musicVolume);
        musicVCA.setVolume(musicVolume); //FMOD music VCA
    }

    public void SetSFX(float sfxVolume)
    {
        audioMixer.SetFloat("sfxVolume", sfxVolume);
        sfxVCA.setVolume(sfxVolume);
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
