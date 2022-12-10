using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class StatsContainer
    {
        public Dictionary<string, int> PrefabSpawnRateDict 
            => _prefabSpawnRateDict ??= GetSpawnRate();
    
        private Dictionary<string, int> _prefabSpawnRateDict;

        Dictionary<string, int> GetSpawnRate()
        {
            List<GameObject> gameObjects = FileManager.LoadAllPrefabs("Assets/Resources/Prefabs/Trash");
            Dictionary<string, int> dict = new Dictionary<string, int>();
            
            foreach (var gameObj in gameObjects)
            {
                dict.Add(gameObj.name, PlayerPrefs.GetInt(gameObj.name));
            }
            return dict;
        }

        public void AddSpawnedPrefab(string name)
        {
            PlayerPrefs.SetInt(name, PlayerPrefs.GetInt(name)+1);

            if (_prefabSpawnRateDict.ContainsKey(name))
                _prefabSpawnRateDict[name] += 1;
            else
                _prefabSpawnRateDict.Add(name, 1);
        }
    }
}


