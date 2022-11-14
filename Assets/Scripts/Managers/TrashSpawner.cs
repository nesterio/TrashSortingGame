using System.Collections;
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

        // Spawn cycle
        public static IEnumerator Spawner()
        {
            float timeToWait = scoreManager.score == 0 ? defaultSpawnRate 
                : defaultSpawnRate / (scoreManager.score * diffictulty);
            timeToWait = Mathf.Max(timeToWait, 1);
            
            WaitForSeconds waitTime = new WaitForSeconds(timeToWait);
            Debug.Log(timeToWait);
            
            while (true)
            {
                if (objectPooler != null)
                {
                    objectPooler.SpawnRandomTrash(trashSpawnPos, Quaternion.identity);
                }

                yield return waitTime;
            }
        }
    }
}
