using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Trash : Grabbable
{
    public TrashType trashType;
    [Space(10)]
    [SerializeField] private bool hasVariation;
    [SerializeField] private GameObject[] variationObjects;
    private GameObject variationObj;
    [Space(10)] [SerializeField] private Quaternion originalRotation = new quaternion();
    [Space(10)]
    [SerializeField] Collider _collider;
    [SerializeField] float _bounciness;
    [SerializeField] float _weight;
    
    private void Start()
    {
        gameObject.tag = "Trash";

        _rigidbody.mass = _weight;

        PhysicMaterial trashMaterial = new PhysicMaterial();        
        trashMaterial.bounciness = _bounciness;
        _collider.material = trashMaterial;
    }
    
    private void OnEnable()
    {
        if(hasVariation && variationObjects.Length > 0)
            variationObj = variationObjects[Random.Range(0, variationObjects.Length)];
        
        if (hasVariation && variationObj != null)
            variationObj.SetActive(Random.Range(0, 1) != 0);

        transform.rotation = originalRotation;
    }

    private void OnDisable()
    {
        if (hasVariation && variationObj != null)
            variationObj.SetActive(true);
    }
}