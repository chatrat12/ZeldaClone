using UnityEngine;

public class Mechanism : MonoBehaviour
{
    public delegate void MechanismEvent(Mechanism mechanism);
    public event MechanismEvent Activated;
    public event MechanismEvent Deactivated;

    protected void OnActivate()
    {
        if (Activated != null)
            Activated(this);
    }
    protected void OnDeactivate()
    {
        if (Deactivated != null)
            Deactivated(this);
    }
}
