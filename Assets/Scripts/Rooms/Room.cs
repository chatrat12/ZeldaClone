using UnityEngine;

public class Room : MonoBehaviour
{
    public delegate void RoomEvent(Room sender, PlayerController player);
    public event RoomEvent PlayerEntered;
    public event RoomEvent PlayerLeft; 

    public void OnPlayerEntered(PlayerController player)
    {
        if (PlayerEntered != null)
            PlayerEntered(this, player);
    }
    public void OnPlayerLeft(PlayerController player)
    {
        if (PlayerLeft != null)
            PlayerLeft(this, player);
    }
}
