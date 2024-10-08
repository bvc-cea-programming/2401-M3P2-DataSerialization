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
        SaveData _saveData = new SaveData();

        // Create an array of placeable objects
        Placeable[] _placeableObjects = new Placeable[placeableGenerator.PlaceableContainer.childCount];
        // Loop through the children of the placeable container
        for (int i = 0; i < _placeableObjects.Length; i++)
        {
            Transform placeableChild = placeableGenerator.PlaceableContainer.GetChild(i);
            PlaceableObject placeableObject = placeableChild.GetComponent<PlaceableObject>();
            Placeable _placeable = new Placeable
            {
                type = placeableObject.Type,
                position = placeableChild.position,
                rotation = placeableChild.eulerAngles,
            };
            _placeableObjects[i] = _placeable;
        }

        //set the array in the save data object
            _saveData.placeableType = _placeableObjects;


        // Save the save data object in playerprefs
        PlayerPrefs.SetString(_saveName, JsonUtility.ToJson(_saveData));
        
        _isLoaded = true;
        Debug.Log("Data Saved");
    }

    private void Load()
    {
        // If data is not loaded, load the data
        if(_isLoaded) return;

        // Get the save data from playerprefs
        string save = PlayerPrefs.GetString(_saveName);
        SaveData _newSave = JsonUtility.FromJson<SaveData>(save);


        // Loop through the placeables and generate them
        foreach (Placeable placeable in _newSave.placeableType)
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
