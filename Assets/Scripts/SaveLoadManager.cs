using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private PlaceableGenerator placeableGenerator;
    private bool _isLoaded = false;
    private string _saveName = "MAIN_SAVE";

    private void Save()
    {
        // Create a save data object
        SaveData mySave = new SaveData();

        // Create an array of placeable objects
        Placeable[] placeablesArray = new Placeable[placeableGenerator.PlaceableContainer.childCount];

        // Loop through the children of the placeable container

        for (int i = 0; i < placeableGenerator.PlaceableContainer.childCount; i++)
        {
            Transform child = placeableGenerator.PlaceableContainer.GetChild(i);
            Placeable placeCurrent = new Placeable();
            placeCurrent.type = child.GetComponent<PlaceableObject>().Type;
            placeCurrent.position = child.position;
            placeCurrent.rotation = child.rotation.eulerAngles;
            placeablesArray[i] = placeCurrent;
        }

        //set the array in the save data object
        mySave.placeableTypes = placeablesArray;

        // Save the save data object in playerprefs
        PlayerPrefs.SetString(_saveName, JsonUtility.ToJson(mySave));

        _isLoaded = true;
        Debug.Log("Data Saved");
    }

    private void Load()
    {
        // If data is not loaded, load the data
        if(_isLoaded) return;

        // Get the save data from playerprefs
        string saveString = PlayerPrefs.GetString(_saveName);
        SaveData loadTemp = JsonUtility.FromJson<SaveData>(saveString);
        // Loop through the placeables and generate them

        foreach (Placeable p in loadTemp.placeableTypes)
        {
            placeableGenerator.GeneratePlaceable(p.type, p.position, p.rotation);
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
