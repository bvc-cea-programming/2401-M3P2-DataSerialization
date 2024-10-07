using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public List<Placeable> placeables;

    // Constructor to initalize the list
    public SaveData()
    {
        placeables = new List<Placeable>();
    }
}
