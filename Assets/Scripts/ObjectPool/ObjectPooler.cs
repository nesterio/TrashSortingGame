using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectPooler : MonoBehaviour
{
    //// Singleton ////
    public static ObjectPooler Instance;
    
    //// Pool structs /////
    //[System.Serializable]
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

    //// Amount of prefabs in a pool /////
    public int TrashPrefabsCount = 3;
    public int PoolPrefabsCount = 5;
    
    [Space(10)]
    
    //// Pools ////
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
    }

    public void Initialize(Action callback = null)
    {
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
        
        callback?.Invoke();
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

        TrashType RandomTrashType()
        {
            int id = Random.Range(0, MainManager.AvailableTrashTypes.Count);
            return MainManager.AvailableTrashTypes[id];
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
