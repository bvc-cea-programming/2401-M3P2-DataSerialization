using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private PlaceableGenerator placeableGenerator;
    private bool _isLoaded = false;
    private string _saveName = "MAIN_SAVE";

    private void Save()
    {
        // Create a save data object
        SaveData myObject = new SaveData();

        // Create an array of placeable objects
        Placeable[] placeables;

        // Loop through the children of the placeable container
        //foreach (Placeable placeable in placeables)
        //{

        //}


        //set the array in the save data object
        string saveName = JsonUtility.ToJson(myObject);

        // Save the save data object in playerprefs
        PlayerPrefs.SetString(saveName, _saveName);
        
        _isLoaded = true;
        Debug.Log("Data Saved");
    }

    private void Load()
    {
        // If data is not loaded, load the data
        if(_isLoaded) return;

        // Get the save data from playerprefs
        PlayerPrefs.GetString(_saveName);

        // Loop through the placeables and generate them
        JsonUtility.FromJson<Type>(_saveName);
        placeableGenerator = new PlaceableGenerator();
       // placeableGenerator.GeneratePlaceable(type, position, rotation);
        //foreach (Placeable placeObject in placeables)
        //{
            
        //}
        
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
