using UnityEngine;

public static class ParticleManager
{
    public static void CreateParticles(Vector3 position, ParticleSettings particleSettings = null)
    {
        if (particleSettings == null)
            particleSettings = new ParticleSettings();
        
        GameObject particleObj;
        
        // TODO: Initialize variables from ParticleSettings into an object from ObjectPool
        particleObj = new GameObject();
        var particleSystem = particleObj.AddComponent(typeof(ParticleSystem)) as ParticleSystem;

        particleObj.transform.position = position;
        
        if(particleSystem != null)
            particleSystem.Play();
    }
}
