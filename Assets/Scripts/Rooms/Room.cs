using UnityEngine;

public class Room : MonoBehaviour
{
    public delegate void RoomEvent(Room sender, GameObject target);
    public event RoomEvent RoomEntered;
    public event RoomEvent RoomLeft; 
}
