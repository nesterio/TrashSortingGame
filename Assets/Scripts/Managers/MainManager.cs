using System.Collections.Generic;
using Managers;
using UnityEngine;
using Scripts.Clicking;
public class MainManager : MonoBehaviour
{
    private ClickDetector ClickDetector;

    public static readonly List<TrashType> AvailableTrashTypes = new List<TrashType>()
    {
        TrashType.Glass,
        TrashType.Organic,
        TrashType.Paper
    };

    void Start()
    {
        ClickDetector = new ClickDetector();
        
        TrashSpawner.InitializeTrashPools(() => 
            ObjectPooler.Instance.Initialize
                (StartSpawning));
    }

    public void StartSpawning()
    {
        if (TrashSpawner.IsSpawning == true) 
            return;
        
        TrashSpawner.IsSpawning = true;
        StartCoroutine(TrashSpawner.Spawner());
    }

    public void PauseSpawning()
    {
        if (!TrashSpawner.IsSpawning) 
            return;
        
        TrashSpawner.IsSpawning = false;
        StopCoroutine(TrashSpawner.Spawner());
    }
    
}
