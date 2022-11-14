using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Trash : Grabbable
{
    public TrashType trashType;
    [Space(5)]
    [SerializeField] private bool hasVariation;

    [Space(10)]
    [SerializeField] private GameObject[] variationObjects;
    private GameObject variationObj;
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
    }

    private void OnDisable()
    {
        if (hasVariation && variationObj != null)
            variationObj.SetActive(true);
    }
}