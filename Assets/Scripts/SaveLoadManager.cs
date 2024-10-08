using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using UnityEngine.UIElements;

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
        int childCount = placeableGenerator.PlaceableContainer.childCount;
        Placeable[] placeableObjects = new Placeable[childCount];

        // Loop through the children of the placeable container
        for (int i = 0; i < childCount; i++)
        {
            Placeable placeable = new Placeable();
            placeable.type = placeableGenerator.PlaceableContainer.GetChild(i).GetComponent<PlaceableObject>().Type;
            placeable.position = placeableGenerator.PlaceableContainer.GetChild(i).position;
            placeable.rotation = placeableGenerator.PlaceableContainer.GetChild(i).eulerAngles;

            placeableObjects[i] = placeable;
            Debug.Log(placeableObjects[i]);
        }

        //set the array in the save data object
        saveData.placeables = placeableObjects;
        // Save the save data object in playerprefs
        PlayerPrefs.SetString(_saveName, JsonUtility.ToJson(saveData));
        _isLoaded = true;
        Debug.Log("Data Saved");
    }

    private void Load()
    {
        // If data is not loaded, load the data
        if (_isLoaded) return;

        // Get the save data from playerprefs
        SaveData saveDataLoader = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(_saveName));
        // Loop through the placeables and generate them
        for (int i = 0; i < saveDataLoader.placeables.Length; i++)
        {
            placeableGenerator.GeneratePlaceable(saveDataLoader.placeables[i].type, saveDataLoader.placeables[i].position, saveDataLoader.placeables[i].rotation);
            Debug.Log("Loading");
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
