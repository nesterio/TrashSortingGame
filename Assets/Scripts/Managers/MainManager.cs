using System.Collections.Generic;
using Scripts.Clicking;
using UnityEngine;

namespace Managers
{
    public class MainManager : MonoBehaviour
    {
        //private ClickMaterialType _clickMaterialType;
    
        private ClickDetector _clickDetector;

        public static GameObject HoldedObject = null;

        public static MainManager Instance;

        public static readonly List<TrashType> AvailableTrashTypes = new List<TrashType>()
        {
            TrashType.Glass,
            TrashType.Organic,
            TrashType.Paper
        };

        void Awake()
        {
            if (Instance != null)
                Destroy(this);
            else
                Instance = this;
        }

        void Start()
        {
            //ClickMaterialType = new ClickMaterialType(); // Зачем это тут?
        
            _clickDetector = new ClickDetector();
        
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
}
