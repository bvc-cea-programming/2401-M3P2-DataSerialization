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
        SaveData myData = new SaveData();

        // Create an array of placeable objects
        Transform placeableContainer = placeableGenerator.PlaceableContainer;
        int childCount = placeableContainer.childCount;
        Placeable[] placeables = new Placeable[childCount];
        
        // Loop through the children of the placeable container
        for (int i = 0;i < childCount;i++)
        {
            Transform child = placeableContainer.GetChild(i);
            PlaceableObject placeableObject = child.GetComponent<PlaceableObject>();
            Placeable placeable = new Placeable
            {
                type = placeableObject.Type,
                position =child.position,
                rotation =child.eulerAngles,
            };
            placeables[i] = placeable;
        }
        
        //set the array in the save data object
        myData.placeables = placeables;

        // Save the save data object in playerprefs
        string myDataString = JsonUtility.ToJson(myData);
        PlayerPrefs.SetString(_saveName, myDataString);
        PlayerPrefs.Save();
        _isLoaded = true;
        Debug.Log("Data Saved");
    }

    private void Load()
    {
        // If data is not loaded, load the data
        if(_isLoaded) return;

        // Get the save data from playerprefs
        string myDataString = PlayerPrefs.GetString(_saveName, null);
        if (string.IsNullOrEmpty(myDataString)) 
        {
            Debug.Log(" myDataString is empty");
            return;
        }
        // Loop through the placeables and generate them



        SaveData loadedData = JsonUtility.FromJson<SaveData>(myDataString);

        // placeableGenerator = new PlaceableGenerator();
        
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
