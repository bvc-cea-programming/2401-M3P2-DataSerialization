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
        
        
        // Create an array of placeable objects
        

        // Loop through the children of the placeable container
        
        
        //set the array in the save data object
        
        
        // Save the save data object in playerprefs
        
        
        _isLoaded = true;
        Debug.Log("Data Saved");
    }

    private void Load()
    {
        // If data is not loaded, load the data
        if(_isLoaded) return;
        
        // Get the save data from playerprefs
        
        
        // Loop through the placeables and generate them
        
        
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
