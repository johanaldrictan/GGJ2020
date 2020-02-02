using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider fxSlider;

    [SerializeField]
    private AudioMixerGroup musicVolume;
    [SerializeField]
    private AudioMixerGroup sfxVolume;
    [SerializeField]
    [Tooltip("Max volume in decibels")]
    private int maxDecibel;
    [SerializeField]
    [Tooltip("Min volume in decibels")]
    private int minDecibel;
    private void Awake()
    {
        //Set default volume levels    
        fxSlider.value = 50;
        musicSlider.value = 50;
        masterSlider.value = 100;
        SetMusicVolume();
        SetSoundEffectsVolume();
        SetMasterVolume();
    }
    public void SetMasterVolume()
    {
        AudioListener.volume = masterSlider.value/100;
    }
    public void SetSoundEffectsVolume()
    {
        sfxVolume.audioMixer.SetFloat("EffectsVolume", Mathf.Log10(fxSlider.value/100)*20);
    }
    public void SetMusicVolume()
    {
        musicVolume.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value / 100) * 20);
    }
}
