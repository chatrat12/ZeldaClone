using UnityEngine;

public class DoorLockedByKey : LockedDoor
{

    private void OnTriggerEnter(Collider other)
    {
        var collection = other.GetComponent<ItemCollection>();
        if (collection != null)
        {
            if (collection.SilverKeyCount > 0)
            {
                Open();
                collection.SilverKeyCount--;
            }
        }
    }
}
