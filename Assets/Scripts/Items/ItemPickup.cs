using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public virtual void AddToCollection(ItemCollection collection)
    {
        Destroy(this.gameObject);
    }
}
