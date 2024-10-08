using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private PlaceableGenerator placeableGenerator;
    private bool _isLoaded = false;
    private string _saveName = "MAIN_SAVE";

    private void Save()
    {
        // Create a save data object
        SaveData saveData = new SaveData();
        
        // Create an array of placeable objects
        Placeable[] placeableArry = new Placeable[placeableGenerator.PlaceableContainer.childCount];
       

        // Loop through the children of the placeable container
        for(int i = 0; i < placeableArry.Length; i++)
        {
            Transform child = placeableGenerator.PlaceableContainer.GetChild(i);
            PlaceableObject placeableObject = child.gameObject.GetComponent<PlaceableObject>();

            Placeable placeable = new Placeable
            {
                position = child.position,
                rotation = child.eulerAngles,
                type = placeableObject.Type,
            };
            placeableArry[i] = placeable;
        }

        //set the array in the save data object
       saveData.PlaceableType = placeableArry;

        // Save the save data object in playerprefs
        PlayerPrefs.SetString(_saveName, JsonUtility.ToJson(saveData));

        _isLoaded = true;
        Debug.Log("Data Saved");
    }

    private void Load()
    {
        // If data is not loaded, load the data
        if(_isLoaded) return;

        // Get the save data from playerprefs
        string saveString = PlayerPrefs.GetString(_saveName);
        SaveData data = JsonUtility.FromJson<SaveData>(saveString);

        // Loop through the placeables and generate them
        foreach(Placeable placeable in data.PlaceableType)
        {
            placeableGenerator.GeneratePlaceable(placeable.type, placeable.position, placeable.rotation);
            
        }
        
        _isLoaded = true;
        Debug.Log("Data Loaded");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            Save();
        }
        
        if (Input.GetKeyDown(KeyCode.F9))
        {
            Load();
        }
    }
}
