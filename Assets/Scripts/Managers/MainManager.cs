using System;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using Scripts.Clicking;
public class MainManager : MonoBehaviour
{
    private ClickDetector ClickDetector;
    private ResolutionManager ResolutionManager;

    public static readonly List<TrashType> AvailableTrashTypes = new List<TrashType>()
    {
        TrashType.Glass,
        TrashType.Organic,
        TrashType.Paper
    };

    void Start()
    {
        ClickDetector = new ClickDetector();
        ResolutionManager = new ResolutionManager();

        var transformPosition = Camera.main.transform.position;
        transformPosition.z = ResolutionManager.CameraPosZ;
        Camera.main.transform.position = transformPosition;

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
