using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : Grabbable
{
    [SerializeField] GameObject trashObject;
    static float jumpTrash;
    static float massTrash;

    private void Start()
    {
        trashObject.tag = "Trash";

        _rigidbody.mass = massTrash;
        _rigidbody.drag = jumpTrash;
    }
}
enum TrashType
{
    Glas,
    Organic
}
