using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private LayerMask interactablesLayer;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryInteract();
        }
    }
    private void TryInteract()
    {
        if (Physics.Raycast(_mainCamera.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2)), out var hit,
                Mathf.Infinity, interactablesLayer))
        {
            var interactor = hit.transform.GetComponent<IInteractable>();
            if (interactor == null) return;

            interactor.Interact();
        }
    }
}
