using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "ClickMaterial")]
public class ClickMaterial : ScriptableObject
{
    [SerializeField] private Color darkColor;
    [SerializeField] private Color lightColor;
    [SerializeField] private AudioMixer sound;
    [SerializeField] private int numberParticle;

    public Color DarkColor => darkColor;
    public Color LightColor => lightColor;
    public AudioMixer Sound => Sound;
    public int NumberParticle => numberParticle;
}
