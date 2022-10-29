using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsManagerUI : MonoBehaviour
{
    [Header("Buttons")]

    [SerializeField] BasicClickableUI LowGraphicsButton;
    [SerializeField] BasicClickableUI MediumGraphicsButton;
    [SerializeField] BasicClickableUI HighGraphicsButton;

    [Space(5)]

    [Header("Sliders")]

    [SerializeField] Slider MusicVolumeSlider;
    [SerializeField] Slider SoundsVolumeSlider;

    [Space(5)]

    [Header("Toggle")]

    [SerializeField] Toggle LowGraphics;
    [SerializeField] Toggle MediumGraphics;
    [SerializeField] Toggle HighGraphics;

    [Header("AudioMixer")]

    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioMixer audioMixer1;

    private void Start()
    {
        InitializeButton();
        CheckSettings();
    }
    private void Update()
    {
        InitializeSliders();
    }
    void InitializeButton()
    {
        var lowGraphicsButton = LowGraphicsButton.GetComponent<BasicClickableUI>();
        if(lowGraphicsButton != null)
        {
            lowGraphicsButton.AssignClickAction(() =>
            {
                LowGraphics.isOn = true;
                SettingsManager.Instance.SetQuality(GraphicsPreset.Low);                
                if(LowGraphics.isOn == true)
                {
                    LowGraphics.isOn = true;
                    MediumGraphics.isOn = false;
                    HighGraphics.isOn = false;
                }
            });
        }
        var mediumGraphicsButton = MediumGraphicsButton.GetComponent<BasicClickableUI>();
        if(mediumGraphicsButton != null)
        {
            mediumGraphicsButton.AssignClickAction(() =>
            {
                MediumGraphics.isOn = true;
                SettingsManager.Instance.SetQuality(GraphicsPreset.Medium);
                if(MediumGraphics.isOn == true)
                {
                    LowGraphics.isOn = false;
                    MediumGraphics.isOn = true;
                    HighGraphics.isOn = false;
                }
            });
        }
        var highGraphicsButton = HighGraphicsButton.GetComponent<BasicClickableUI>();
        if(highGraphicsButton != null)
        {
            highGraphicsButton.AssignClickAction(() =>
            {
                HighGraphics.isOn = true;
                SettingsManager.Instance.SetQuality(GraphicsPreset.High);
                if(HighGraphics.isOn == true)
                {
                    HighGraphics.isOn = true;
                    LowGraphics.isOn = false;
                    MediumGraphics.isOn = false;
                }   
            });
        }
    }
    void InitializeSliders()
    {
        if(MusicVolumeSlider != null)
        {
            SettingsManager.Instance.SetVolumeMusic(PlayerPrefs.GetFloat("MusicPrefs"), audioMixer);
            PlayerPrefs.SetFloat("MusicPrefs", MusicVolumeSlider.value);
            MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicPrefs");
        }
        if(SoundsVolumeSlider != null)
        {
            SettingsManager.Instance.SetVolumeSound(PlayerPrefs.GetFloat("SoundPrefs"), audioMixer1);
            PlayerPrefs.SetFloat("SoundPrefs", SoundsVolumeSlider.value);
            SoundsVolumeSlider.value = PlayerPrefs.GetFloat("SoundPrefs");          
        }
          
    }

    private void SelectQuality(GraphicsPreset quality)
    {
        switch (quality)
        {
            default:
            case GraphicsPreset.Low:

                break;
            case GraphicsPreset.Medium:

                break;
            case GraphicsPreset.High:

                break;
        }

        SettingsManager.Instance.SetQuality(quality);
    }
    public void CheckSettings()
    {
        if (MusicVolumeSlider.value == 1)
        {
            MusicVolumeSlider.value = PlayerPrefs.GetFloat("MusicPrefs");
        }
        if(SoundsVolumeSlider.value == 1)
        {
            SoundsVolumeSlider.value = PlayerPrefs.GetFloat("SoundPrefs");
        }
    }
}








