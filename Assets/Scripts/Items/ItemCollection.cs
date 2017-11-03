using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public int SilverKeyCount { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        var pickup = other.GetComponent<ItemPickup>();
        if (pickup != null)
            pickup.AddToCollection(this);
    }

}
