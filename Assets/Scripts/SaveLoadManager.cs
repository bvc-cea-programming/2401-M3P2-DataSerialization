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
        Placeable[] placeables = new Placeable[placeableGenerator.PlaceableContainer.childCount];

        // Loop through the children of the placeable container
        for (int i = 0; i < placeableGenerator.PlaceableContainer.childCount; i++)
        {
            Transform child = placeableGenerator.PlaceableContainer.GetChild(i);
            PlaceableObject placeableObject = child.GetComponent<PlaceableObject>();

            if (placeableObject != null)
            {
                placeables[i] = new Placeable
                {
                    type = placeableObject.Type,
                    position = child.position,
                    rotation = child.rotation.eulerAngles
                };
            }
        }

        //set the array in the save data object
        saveData.placeables = placeables;

        string json = JsonUtility.ToJson(saveData);

        // Save the save data object in playerprefs

        PlayerPrefs.SetString(_saveName, json);
        PlayerPrefs.Save();
        
        _isLoaded = true;
        Debug.Log("Data Saved");
    }

    private void Load()
    {
        // If data is not loaded, load the data
        if(_isLoaded) return;

        // Get the save data from playerprefs
        string json = PlayerPrefs.GetString(_saveName, string.Empty);
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log("No Save Data found");
            return;
        }

        SaveData loadedData = JsonUtility.FromJson<SaveData>(json);

        // Loop through the placeables and generate them
         foreach (Placeable placeable in loadedData.placeables)
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
