using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : ISwitchable
{
    private bool isActive;
    public override bool IsActive => isActive;
    [SerializeField] private GameObject doorObject;

    public override void SwitchActive()
    {
        isActive = true;
        gameObject.SetActive(false);
    }

    public override void SwitchDeactive()
    {
        isActive = false;
        gameObject.SetActive(true);
    }
}
