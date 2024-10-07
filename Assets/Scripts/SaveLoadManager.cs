using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Transform container = placeableGenerator.PlaceableContainer;
        int numPlaceables = container.childCount;
        Placeable[] placeables = new Placeable[numPlaceables];

        // Loop through the children of the placeable container
        for (int i = 0; i < numPlaceables; i++)
        {
            Transform placeableTransform = container.GetChild(i);
            PlaceableObject placeableObject = placeableTransform.GetComponent<PlaceableObject>();
        }

        //set the array in the save data object
        saveData.placeables = placeables;
        
        // Save the save data object in playerprefs
        string jsonData = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(_saveName, jsonData);
        PlayerPrefs.Save();
        
        _isLoaded = true;
        Debug.Log("Data Saved");
    }

    private void Load()
    {
        // If data is not loaded, load the data
       
        
        // Get the save data from playerprefs
      
        // Loop through the placeables and generate them
    
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
