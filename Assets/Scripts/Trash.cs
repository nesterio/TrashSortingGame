using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Grabbable
{
    [SerializeField] GameObject trashObject;
    [SerializeField] Collider _collider;
    private PhysicMaterial _physicMaterial;
    [SerializeField]private float Bounciness;
    [SerializeField]private float massTrash;


    private void Start()
    {
        trashObject.tag = "Trash";

        _rigidbody.mass = massTrash;

        PhysicMaterial trashMaterial = new PhysicMaterial();        
        trashMaterial.bounciness = Bounciness;
        _collider.material = trashMaterial;
        _physicMaterial = trashMaterial;
    }
}
enum TrashType
{
    Glass,
    Organic
}
