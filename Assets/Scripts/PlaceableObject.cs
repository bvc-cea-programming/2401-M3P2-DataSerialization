using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    [SerializeField] private string type;

    public string Type => type;
}
