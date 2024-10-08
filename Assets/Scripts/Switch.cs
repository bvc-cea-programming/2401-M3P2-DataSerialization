using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    [SerializeField] private ISwitchable switchable;
    public void Interact()
    {
        if (switchable.IsActive)
        {
            switchable.SwitchDeactive();
        }
        else
        {
            switchable.SwitchActive();
        }
    }
}
