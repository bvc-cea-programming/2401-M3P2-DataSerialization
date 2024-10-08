using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    [SerializeField] private PlaceableGenerator placeableGenerator;
    private bool _isLoaded = false;
    private string _saveName = "MAIN_SAVE";
    private Type _type;
    private string type;
    private Vector3 position;
    private Vector3 rotation;
    private Vector3 scale;
    private object placeableContainer;
    private bool placeablesArray;
    private object mySave;
    private object placeCurrent;
   
    private void Save()
    {
        // Create a save data object
        SaveData saveData = new SaveData();
        Placeable[] placeableArrayManager = new Placeable[transform.childCount];

        // Create an array of placeable objects
        for (int i = 0; i < transform.childCount; i++)
        {
        
            {
                placeableArrayManager[i].position = transform.GetChild(i).gameObject.transform.position;
                placeableArrayManager[i].rotation = new Vector3(transform.GetChild(i).gameObject.transform.rotation.x, transform.GetChild(i).gameObject.transform.rotation.y, transform.GetChild(i).gameObject.transform.rotation.z);
                placeableArrayManager[i].type = transform.GetChild(i).gameObject.GetComponent<PlaceableObject>().Type;
            }
            
        }

        // Loop through the children of the placeable container

        for (int i = 0; i < placeableGenerator.PlaceableContainer.childCount; i++)
        {
            Transform child = placeableGenerator.PlaceableContainer.GetChild(i);
            Public Placeable;
            {
                type = child.GetComponent<PlaceableObject>().Type;
                position = child.position;
                rotation = child.rotation.eulerAngles;
            }
            placeablesArray[i] = placeCurrent;
        }

        //set the array in the save data object
        mySave.placeableTypes = placeablesArray;

        // Save the save data object in playerprefs PlayerPrefs.SetString(_saveName, JsonUtility.ToJson(mySave)); PlayerPrefs.SetString(_saveName, JsonUtility.ToJson(mySave));

        _isLoaded = true;
        Debug.Log("Data Saved");
    }

    private void Load()
    {
        // If data is not loaded, load the data
        if (_isLoaded) return;

        // Get the save data from playerprefs
        string saveString = PlayerPrefs.GetString(_saveName);
        SaveData loadTemp = JsonUtility.FromJson<SaveData>(saveString);
        // Loop through the placeables and generate them

        foreach (Placeable p in loadTemp.placeableTypes)
        {
            placeableGenerator.GeneratePlaceable(type, position, rotation);
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