using System.Collections.Generic;

namespace Managers
{
    public class StatsContainer
    {
        public Dictionary<string, int> PrefabSpawnRateDict => _prefabSpawnRateDict ??= GetSpawnRate();
    
        private Dictionary<string, int> _prefabSpawnRateDict;

        Dictionary<string, int> GetSpawnRate()
        {
            // TODO: Брать все возможные имена префабов и по их именам искать в PlayerPrefs.GetInt("Имя префаба")
            // Так заполнить Dictionary
            return null;
        }
    }
}


