using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ClickMaterialType : MonoBehaviour
{
    private Color _darkColor;
    private Color _lightColor;
    private AudioMixer _sound;
    private int _numberParticle;
    private void Start()
    {
        var allParticles = Resources.LoadAll<ClickMaterial>("");
        foreach (var particle in allParticles)
        {
            _darkColor = particle.DarkColor;
            _lightColor = particle.LightColor;
            _sound = particle.Sound;
            _numberParticle = particle.NumberParticle;
        }
    }
}
