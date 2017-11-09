using UnityEngine;

public class Doorway : MonoBehaviour
{
    [SerializeField]
    private Doorway _opposingDoorway;
    private Room _room;

    private void Awake()
    {
        _room = GetComponentInParent<Room>();
        if(_opposingDoorway == null)
        {
            var detecotor = GetComponentInChildren<DoorwayDetector>();
            if (detecotor != null)
                _opposingDoorway = detecotor.FindDoorway();
        }
        if(_opposingDoorway == null)
        {
            Debug.Log("Could not find opposing doorway");
        }
    }

    public void PlayerStartedEntering(PlayerController player)
    {
        player.Movement.FinishedAutoWalking += PlayerFinishedEntering;
    }
    private void PlayerFinishedEntering(PlayerController player)
    {
        player.Movement.FinishedAutoWalking -= PlayerFinishedEntering;
        if (_room != null)
            _room.OnPlayerEntered(player);
    }
    private void PlayerStartedLeaving(PlayerController player)
    {
        player.Movement.FinishedAutoWalking += PlayerFinishedLeaving;
    }
    private void PlayerFinishedLeaving(PlayerController player)
    {
        player.Movement.FinishedAutoWalking -= PlayerFinishedLeaving;
        if (_room != null)
            _room.OnPlayerLeft(player);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();

        if (player != null && _opposingDoorway != null)
        {
            if (!player.Movement.IsAutoWalking) // Make sure player is not already walking through doorway
            {
                PlayerStartedLeaving(player);
                _opposingDoorway.PlayerStartedEntering(player);
                player.Movement.AutoWalkTo(GetAutoWalkTargetForOpposingDoor(player.transform));
            }
        }
    }

    private Vector3 GetAutoWalkTargetForOpposingDoor(Transform playerTransform)
    {
        var targetPosition = _opposingDoorway.transform.position + _opposingDoorway.transform.forward;
        var relativeTargetPosition = targetPosition - playerTransform.transform.position;
        var targetDirection = _opposingDoorway.transform.forward;
        relativeTargetPosition = Vector3.Project(relativeTargetPosition, targetDirection);

        return playerTransform.transform.position + relativeTargetPosition;
    }

}
