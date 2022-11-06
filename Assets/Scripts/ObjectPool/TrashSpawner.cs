using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    private void FixedUpdate()
    {
        ObjectPooler.Instance.SpawnFromPool("Trash", transform.position, Quaternion.identity);
    }

}
