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
        //Pick a random placeable and Instantiate it inside the placeable container
        int randomIndex = Random.Range(0, placeableObjects.Length);
        PlaceableObject randomPlaceable = placeableObjects[randomIndex];

        Instantiate(randomPlaceable, position, Quaternion.identity, placeableContainer);
    
    }

    private void UpdatePlaceableDictionary()
    {
        //Iterate through the array of placeable objects and add them to the dictionary
        foreach (PlaceableObject placeable in placeableObjects)
        {
            if (!_placeableDictionary.ContainsKey(placeable.name))
            {
                _placeableDictionary.Add(placeable.name, placeable);
            }
        }
    }

    public void GeneratePlaceable(string type, Vector3 position, Vector3 rotation = default)
    {
        //Get the placeable based on the type from the dictionary if available, and instantiate the correct placeable object with the correct position and rotation values
        if (_placeableDictionary.TryGetValue(type, out PlaceableObject placeable))
        {
            Quaternion placeableRotation = rotation == Vector3.zero ? Quaternion.identity : Quaternion.Euler(rotation);
            
            //and place it inside the placeable container
            Instantiate(placeable, position, placeableRotation, placeableContainer);
        }
    }
}
