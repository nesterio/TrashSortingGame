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
        // Trash tag is the name of the tag //
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
                poolDict.Add(pool.prefab.name, objectPool);
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
            
            objectPoolsDict.Add(pool.prefab.name, objectPool);
        }
        
        callback?.Invoke();
    }

    public GameObject SpawnRandomTrash(Vector3 position)
    {
        //// TODO: DEFINE UNLOCKED TRASH TYPES
        var trashType = RandomTrashType();
        while (trashPoolTypesDict.ContainsKey(trashType) == false)
        {
            trashType = RandomTrashType();
        }
        var poolDict = trashPoolTypesDict[trashType];
        //// //// ////

        var poolTag = RandomValues(poolDict).First();

        GameObject objectToSpawn = poolDict[poolTag].Dequeue();

        // If there are not enough objects in object pool // TODO: Might need optimization
        if (objectToSpawn.gameObject.activeSelf)
        {
            poolDict[poolTag].Enqueue(objectToSpawn);

            var trash = objectToSpawn.GetComponent<Trash>();

            objectToSpawn = PrefabToSpawn(trash.trashType, trash.prefabName);
            //trash = null;

            objectToSpawn = Instantiate(objectToSpawn, Vector3.zero, Quaternion.identity);
            objectToSpawn.SetActive(false);
        }
        
        objectToSpawn.transform.position = position;
        objectToSpawn.SetActive(true);

        trashPoolTypesDict[trashType][poolTag].Enqueue(objectToSpawn);

        return objectToSpawn;

        GameObject PrefabToSpawn(TrashType trashType, string tag)
        {
            foreach (var poolType in trashPoolTypes)
            {
                if (poolType.trashType == trashType)
                {
                    foreach (var poolTypePool in poolType.pools)
                    {
                        if (poolTypePool.prefab.name == tag)
                        {
                            return poolTypePool.prefab;
                        }
                    }
                }
            }

            Debug.LogError("Couldn't find prefab");
            return null;
        }

        TrashType RandomTrashType()
        {
            int id = Random.Range(0, MainManager.AvailableTrashTypes.Count);
            return MainManager.AvailableTrashTypes[id];
        }
    }
    
    private static IEnumerable<TKey> RandomValues<TKey, TValue>(IDictionary<TKey, TValue> dict)
    {
        var rand = new System.Random();
        var values = dict.Keys.ToList();
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
