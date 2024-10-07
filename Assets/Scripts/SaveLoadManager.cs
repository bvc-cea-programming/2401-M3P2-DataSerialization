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

        // Loop through the children of the placeable container
        foreach (Transform child in container)
        {
            PlaceableObject placeableObject = child.GetComponent<PlaceableObject>();
            if (placeableObject != null)
            {
                // Store the type, position, and rotation
                Placeable placeable = new Placeable(
                    placeableObject.Type,
                    child.position,
                    child.rotation.eulerAngles
                );

                // Add the placeable to the save data list
                saveData.placeables.Add( placeable);
            }
        }
        
        // Convert the save data object to JSON
        string jsonData = JsonUtility.ToJson(saveData);

        // Save the JSON in PlayerPrefs
        PlayerPrefs.SetString(_saveName, jsonData);
        PlayerPrefs.Save();
        
        
        _isLoaded = true;
        Debug.Log("Data Saved");
    }

    private void Load()
    {
        // If data is not loaded, load the data
        if(_isLoaded) return;
        
        // Get the save data from playerprefs
        if (PlayerPrefs.HasKey(_saveName))
        {
            string jsonData = PlayerPrefs.GetString(_saveName);

            // Deserialize the JSON to a SaveData obejct
            SaveData loadedData = JsonUtility.FromJson<SaveData>(jsonData);

            // Loop through the placeables and generate them
            foreach (Placeable placeable in loadedData.placeables)
            {
                placeableGenerator.GeneratePlaceable(
                    placeable.type,
                    placeable.position,
                    placeable.rotation
                );
            }
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
