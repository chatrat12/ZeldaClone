using UnityEngine;

public class ActivateCharacterOnRoomEnter : MonoBehaviour
{
    private Room _room;
    private Character _character;

    private bool _hookedEvents = false;

    private void Awake()
    {
        _room= GetComponentInParent<Room>();
        _character = GetComponent<Character>();
        if(_room != null && _character != null)
        {
            _hookedEvents = true;
            _character.enabled = false;

            _room.PlayerEntered += PlayerEnteredRoom;
            _room.PlayerLeft += PlayerLeftRoom;
        }

        // Component does not need to update so disable it.
        enabled = false;
    }

    private void PlayerEnteredRoom(Room sender, PlayerController player)
    {
        _character.enabled = true;
    }
    private void PlayerLeftRoom(Room sender, PlayerController player)
    {
        _character.enabled = false;
    }

    private void OnDestroy()
    {
        if (_hookedEvents)
        {
            _room.PlayerEntered -= PlayerEnteredRoom;
            _room.PlayerLeft -= PlayerLeftRoom;
        }
    }
}
