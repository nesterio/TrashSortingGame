using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Grabbable
{
    [SerializeField] GameObject trashObject;
    [SerializeField]private float jumpingAbilityTrash;
    [SerializeField]private float massTrash;

    private void Start()
    {
        trashObject.tag = "Trash";

        _rigidbody.mass = massTrash;
        _rigidbody.drag =jumpingAbilityTrash;
    }
}
enum TrashType
{
    Glass,
    Organic
}
