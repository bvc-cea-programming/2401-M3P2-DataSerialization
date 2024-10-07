using UnityEngine;
using System;
using JetBrains.Annotations;
[Serializable]
public class Placeable
{
    public string type;
    public Vector3 position;
    public Vector3 rotation; 

    public Placeable(string type, Vector3 position, Vector3 rotation)
    {
        this.type = type;
        this.position = position;
        this.rotation = rotation;
    }
}
