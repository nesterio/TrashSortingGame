using Managers;
using UnityEngine;
using Scripts.Clicking;
public class MainManager : MonoBehaviour
{
    private ClickDetector ClickDetector;

    void Start()
    {
        ClickDetector = new ClickDetector();

        StartSpawning();
    }

    public void StartSpawning()
    {
        if (TrashSpawner.IsSpawning != false) 
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
