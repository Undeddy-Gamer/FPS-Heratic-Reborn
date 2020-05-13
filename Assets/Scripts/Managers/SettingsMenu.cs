using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer masterAudioMixer;
    public void SetMasterVolume(float volume)
    {
        masterAudioMixer.SetFloat("masterVolume", volume);
    }
        

}
