using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public static class TrashSpawner
    {
        public static bool IsSpawning;
        
        // References
        private static ScoreManager scoreManager = ScoreManager.Instance;
        private static ObjectPooler objectPooler = ObjectPooler.Instance;

        // Spawning
        private static float defaultSpawnRate = 3;
        private static float diffictulty = 0.001f;

        private static Vector3 trashSpawnPos = new Vector3(0, 4.4f, -1.7f);

        public static void InitializeTrashPools(Action callback = null)
        {
            // Create a pool of pools of trash
            var trashPoolTypes = new List<ObjectPooler.PoolType>();
            // Go through available trash types
            foreach (var type in MainManager.AvailableTrashTypes)
            {
                var trashOfType = FileManager.LoadAllPrefabs("Trash/" + type);

                var pools = new List<ObjectPooler.Pool>();

                // Create pools of trash of current type
                foreach (var trash in trashOfType)
                {
                    pools.Add(new ObjectPooler.Pool() 
                    {
                            //tag = trash.name,
                            prefab = trash
                    });
                }

                // Create a pool of trash pools
                var poolType = new ObjectPooler.PoolType()
                {
                    trashType = type,
                    pools = pools.ToArray()
                };
                
                trashPoolTypes.Add(poolType);
            }

            objectPooler.trashPoolTypes = trashPoolTypes;
            callback?.Invoke();
        }

        // Spawn cycle
        public static IEnumerator Spawner(Action callback = null)
        {
            float timeToWait = scoreManager.Score == 0 ? defaultSpawnRate 
                : defaultSpawnRate / (scoreManager.Score * diffictulty);
            timeToWait = Mathf.Max(timeToWait, 1);
            
            WaitForSeconds waitTime = new WaitForSeconds(timeToWait);
            Debug.Log(timeToWait);
            
            while (true)
            {
                if (objectPooler != null)
                {
                    objectPooler.SpawnRandomTrash(trashSpawnPos);
                }

                yield return waitTime;
            }
            
            callback?.Invoke();
        }
    }
}
