using System.Collections.Generic;
using Managers;
using Particle;
using UnityEngine;
using Scripts.Clicking;
public class MainManager : MonoBehaviour
{
    private ClickMaterialType ClickMaterialType;
    
    private ClickDetector ClickDetector;

    public static readonly List<TrashType> AvailableTrashTypes = new List<TrashType>()
    {
        TrashType.Glass,
        TrashType.Organic,
        TrashType.Paper
    };

    void Start()
    {
        ClickMaterialType = new ClickMaterialType(); //starting the particle system
        
        ClickDetector = new ClickDetector();
        
        TrashSpawner.InitializeTrashPools(() => 
            ObjectPooler.Instance.Initialize
                (StartSpawning));
    }

    public void StartSpawning()
    {
        if (TrashSpawner.IsSpawning) 
            return;
        
        TrashSpawner.IsSpawning = true;
        StartCoroutine(TrashSpawner.Spawner(StartSpawning));
    }

    public void PauseSpawning()
    {
        if (!TrashSpawner.IsSpawning) 
            return;
        
        TrashSpawner.IsSpawning = false;
        StopCoroutine(TrashSpawner.Spawner());
    }
    
}
