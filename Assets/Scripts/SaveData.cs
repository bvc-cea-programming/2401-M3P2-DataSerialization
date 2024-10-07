using System;

[Serializable]
public class SaveData
{
    public Placeable[] placeables;

    public SaveData() { }
    public SaveData(Placeable[] placeables)
    {
        this.placeables = placeables;
    }
}
