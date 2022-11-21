using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class SettingsManager
{
    public static SettingsManager Instance { 
        get { if(_instance == null)
            {
                _instance = new SettingsManager();
            }
            return _instance;
        } }

    private static SettingsManager _instance;
   
    public void SetQuality(GraphicsPreset quality)
    {
        switch (quality)
        {
            default:
                QualitySettings.SetQualityLevel(1);
                break;
            case GraphicsPreset.Low:
                QualitySettings.SetQualityLevel(0);
                break;
            case GraphicsPreset.High:
                QualitySettings.SetQualityLevel(2);
                break;
            case GraphicsPreset.Medium:
                QualitySettings.SetQualityLevel(1);
                break;
        }
    }

    public void SetVolumeMusic(float MusicVolume, AudioMixer mixer)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(MusicVolume) * 20);
    }

    public void SetVolumeSound(float SoundVolume, AudioMixer mixer)
    {
        mixer.SetFloat("SoundVolume", Mathf.Log10(SoundVolume) * 20);
    }
}
public enum GraphicsPreset
{
    Low,
    Medium,
    High
}
