using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private PlaceableGenerator placeableGenerator;
    private bool _isLoaded = false;
    private string _saveName = "MAIN_SAVE";
    [SerializeField] GameObject container;

    private void Save()
    {
        // Create a save data object
        SaveData saveData = new SaveData();

        // Create an array of placeable objects
        Placeable[] placeablesList = new Placeable[container.transform.childCount];

        // Loop through the children of the placeable container
        for (int i = 0; i < placeablesList.Length; i++)
        {
            placeablesList[i] = new Placeable();
            placeablesList[i].position= container.transform.GetChild(i).position;
            placeablesList[i].rotation = new Vector3(container.transform.GetChild(i).transform.rotation.x, container.transform.GetChild(i).transform.rotation.y, container.transform.GetChild(i).transform.rotation.z);
            placeablesList[i].type=container.transform.GetChild(i).GetComponent<PlaceableObject>().Type;
        }

        
        //set the array in the save data object
        saveData.placeableArray = placeablesList;

        
        // Save the save data object in playerprefs
        var json = JsonUtility.ToJson(saveData);
        PlayerPrefs.SetString(_saveName, json);
        
        _isLoaded = true;
        Debug.Log("Data Saved");
    }

    private void Load()
    {
        // If data is not loaded, load the data
        if(_isLoaded) return;
        var json = PlayerPrefs.GetString(_saveName);
        SaveData temporarySaveData = JsonUtility.FromJson<SaveData>(json);
        for (int i = 0;i < temporarySaveData.placeableArray.Length;i++)
        {
            placeableGenerator.GeneratePlaceable(temporarySaveData.placeableArray[i].type, temporarySaveData.placeableArray[i].position, temporarySaveData.placeableArray[i].rotation);
        }
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
