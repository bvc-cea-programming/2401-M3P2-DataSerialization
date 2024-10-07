using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableGenerator : MonoBehaviour
{
    
    [SerializeField] private PlaceableObject[] placeableObjects;
    [SerializeField] private LayerMask placementLayerMask;
    [SerializeField] private Transform placeableContainer;
    
    private Camera _mainCamera;
    private Dictionary<string, PlaceableObject> _placeableDictionary = new Dictionary<string, PlaceableObject>();

    public Transform PlaceableContainer => placeableContainer;
    
    private void Start()
    {
        _mainCamera = Camera.main;
        UpdatePlaceableDictionary();
    }

    private void Update()
    {
        if(placeableObjects.Length == 0) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            PlaceRandomPlaceableOnClick();
        }
    }

    private void PlaceRandomPlaceableOnClick()
    {
        if (Physics.Raycast(_mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2)), out var hit,
                Mathf.Infinity, placementLayerMask))
        {
            PlaceRandomPlaceable(hit.point);
        }
    }

    private void PlaceRandomPlaceable(Vector3 position)
    {
        // Pick a random object from the array
        PlaceableObject randomPlaceable = placeableObjects[Random.Range(0, placeableObjects.Length)];

        // Instantiate the object at the given position and make it a child of the placeable container
        Instantiate(randomPlaceable, position, Quaternion.identity, placeableContainer);
    }

    private void UpdatePlaceableDictionary()
    {
        foreach (PlaceableObject placeable in placeableObjects)
        {
            if (!_placeableDictionary.ContainsKey(placeable.Type))
            {
                _placeableDictionary.Add(placeable.Type, placeable);
                Debug.Log($"Added {placeable.Type} to the dictionary");
            }
        }
    }

    public void GeneratePlaceable(string type, Vector3 position, Vector3 rotation = default)
    {
        if (_placeableDictionary.TryGetValue(type, out PlaceableObject placeable))
        {
            Instantiate(placeable, position, Quaternion.Euler(rotation), placeableContainer);
        }
        else
        {
            Debug.LogWarning($"Placeable type '{type}' not found in the dictionary!");
        }
    }
}
