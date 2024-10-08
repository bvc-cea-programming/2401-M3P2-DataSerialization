using UnityEngine;
using System;

[Serializable]
public class Placeable
{
    public string type;         // Type of the object (such as cube, sphere, capsule, etc)
    public Vector3 position;    // Object's position
    public Vector3 rotation;    // Object's rotation

    // Constructor to easily assign values
    public Placeable(string type, Vector3 position, Vector3 rotation)
    {
        this.type = type;
        this.position = position;
        this.rotation = rotation;
    }
}