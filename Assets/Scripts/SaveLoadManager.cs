using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private PlaceableGenerator placeableGenerator;
    private bool _isLoaded = false;
    private string _saveName = "MAIN_SAVE";
    [SerializeField] GameObject placeableContainer;
    private void Save()
    {
        // Create a save data object
        SaveData saveData = new SaveData();
        Placeable[] placeableArrayManager = new Placeable[placeableContainer.transform.childCount];
        // Create an array of placeable objects
        for(int i = 0; i<placeableContainer.transform.childCount; i++)
        {
            placeableArrayManager[i] = new Placeable();
            placeableArrayManager[i].position = placeableContainer.transform.GetChild(i).gameObject.transform.position;
            placeableArrayManager[i].rotation = new Vector3(placeableContainer.transform.GetChild(i).gameObject.transform.rotation.x, placeableContainer.transform.GetChild(i).gameObject.transform.rotation.y, placeableContainer.transform.GetChild(i).gameObject.transform.rotation.z);
            placeableArrayManager[i].type = placeableContainer.transform.GetChild(i).gameObject.GetComponent<PlaceableObject>().Type;
        }

        // Loop through the children of the placeable container
        saveData.placeableArray = placeableArrayManager;

        //set the array in the save data object


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
        
        // Get the save data from playerprefs
        var json = PlayerPrefs.GetString(_saveName);
       SaveData tempSave = JsonUtility.FromJson<SaveData>(json);
        
        // Loop through the placeables and generate them
        for(int i = 0; i < tempSave.placeableArray.Length; i++)
        {
            placeableGenerator.GeneratePlaceable(tempSave.placeableArray[i].type, tempSave.placeableArray[i].position, tempSave.placeableArray[i].rotation);
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
