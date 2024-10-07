using UnityEngine;
using System;

public class Placeable
{
    public string Type;     
    public Vector3 Position; 
    public Vector3 Rotation; 
    public Placeable(string type, Vector3 position, Vector3 rotation)
    {
        this.Type = type;
        this.Position = position;
        this.Rotation = rotation;
    }
}
