using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    public int TrashPrefabsCount = 3;
    public int PoolPrefabsCount = 5;

    [System.Serializable]
    public struct PoolType
    {
        public TrashType trashType;
        public Pool[] pools;
    }

    [System.Serializable]
    public struct Pool
    {
        [InspectorName("Tag")]public string tag;
        [InspectorName("Prefab")]public GameObject prefab;
    }

    public List<Pool> objectPools;
    private Dictionary<string, Queue<GameObject>> objectPoolsDict;
    [Space(5)]
    public List<PoolType> trashPoolTypes;
    private Dictionary<TrashType, Dictionary<string, Queue<GameObject>>> trashPoolTypesDict;
    

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        //// Initialize trash object pool ////
        trashPoolTypesDict = new Dictionary<TrashType, Dictionary<string, Queue<GameObject>>>();
        foreach (var poolType in trashPoolTypes)
        {
            var poolDict = new Dictionary<string, Queue<GameObject>>();
            foreach(Pool pool in poolType.pools)
            {
                Queue<GameObject> objectPool = new Queue<GameObject>();

                for(int i = 0; i < TrashPrefabsCount; i++)
                {
                    if(pool.prefab == null)
                        continue;
                    
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);               
                }
                
                poolDict.Add(pool.tag, objectPool);
            }
            trashPoolTypesDict.Add(poolType.trashType, poolDict);
        }
        
        //// Initialize regular object pool ////
        objectPoolsDict = new Dictionary<string, Queue<GameObject>>();
        foreach(Pool pool in objectPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < PoolPrefabsCount; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);               
            }
            
            objectPoolsDict.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnRandomTrash(Vector3 position, Quaternion rotation)
    {
        Debug.Log("trying to spawn trash");

        //// TODO: DEFINE UNLOCKED TRASH TYPES
        var trashType = RandomTrashType();
        while (trashPoolTypesDict.ContainsKey(trashType) == false)
        {
            trashType = RandomTrashType();
        }
        var poolDict = trashPoolTypesDict[trashType];
        //// //// ////
        
        var pool = new Queue<GameObject>(RandomValues(poolDict).First());
        
        GameObject objectToSpawn = pool.Dequeue();
        
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        trashPoolTypesDict[trashType][tag].Enqueue(objectToSpawn);

        return objectToSpawn;

        TrashType RandomTrashType() // TODO: Improve by checking what trash types have been unlocked. Also needs to be organized
        {
            switch (Random.Range(0, 6))
            {
                default:
                case 0:
                    return TrashType.Glass;
                case 1:
                    return TrashType.Metal;
                case 2:
                    return TrashType.Mixed;
                case 3:
                    return TrashType.Organic;
                case 4:
                    return TrashType.Paper;
                case 5:
                    return TrashType.Plastic;
                case 6:
                    return TrashType.Valuable;
            }
        }
    }
    
    private static IEnumerable<TValue> RandomValues<TKey, TValue>(IDictionary<TKey, TValue> dict)
    {
        var rand = new System.Random();
        var values = dict.Values.ToList();
        var size = dict.Count;
        while(true)
        {
            yield return values[rand.Next(size)];
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (objectPoolsDict == null || objectPoolsDict.Count == 0)
        {
            Debug.LogWarning("Object pool is empty!");
            return null;
        }
        
        if (!objectPoolsDict.ContainsKey(tag))
        {
            Debug.LogWarning("Warning!" + tag + "doesn`t exist");
            return null;
        }

        GameObject objectToSpawn = objectPoolsDict[tag].Dequeue();
        
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        objectPoolsDict[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
