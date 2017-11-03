using UnityEngine;

public class DoorLockedByMechanism : LockedDoor
{
    [SerializeField]
    private Mechanism _mechanism;

    protected override void Awake()
    {
        base.Awake();
        _mechanism.Activated += MechanismActivated;
    }

    private void MechanismActivated(Mechanism mechanism)
    {
        Open();
    }
}
