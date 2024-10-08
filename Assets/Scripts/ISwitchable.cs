using UnityEngine;

public abstract class ISwitchable : MonoBehaviour
{
    public abstract bool IsActive { get; }
    public abstract void SwitchActive();

    public abstract void SwitchDeactive();
}
