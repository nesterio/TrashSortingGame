using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public static class FileManager
{
    private const string defaultPrefabPath = "Prefabs";

    public static List<GameObject> LoadAllPrefabs(string path)
    {
        Debug.Log(defaultPrefabPath + "/" + path);
        return Resources.LoadAll<GameObject>(defaultPrefabPath + "/" + path).ToList();
    }

    public static GameObject LoadAndCreatePrefab(string prefabPathOrName, Vector3 position)
    {
        var prefab = LoadPrefab(prefabPathOrName);

        if (prefab == null)
            return null;

        return GameObject.Instantiate(prefab, position, quaternion.identity);
    }

    public static GameObject LoadPrefab(string prefabPathOrName) =>
        LoadObjectOfType<GameObject>(defaultPrefabPath + "/" + prefabPathOrName);
    
    public static T LoadObjectOfType<T>(string path) where T : class
    {
        var loadedObject = Resources.Load(path) as T;
        if (loadedObject == null)
        {
            throw new FileNotFoundException("Couldn't find a prefab at path: " + path);
        }
        return loadedObject;
    }


}
