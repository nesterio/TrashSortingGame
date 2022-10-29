using UnityEngine;

public class ParticleSettings
{
    public Color Color1;
    public Color Color2;
    public float ParticleAmount;
    public AudioClip ClickSound;

    public void InitializeFromScriptableObject()
    {
        // TODO
    }

    public void Initialize(Color color1, Color color2, float particleAmount, AudioClip clickSound = null)
    {
        Color1 = color1;
        Color2 = color2;
        ParticleAmount = particleAmount;
        ClickSound = clickSound;
    }

    public ParticleSettings()
    {
        Initialize(Color.gray,Color.white, 1f);
    }
}
