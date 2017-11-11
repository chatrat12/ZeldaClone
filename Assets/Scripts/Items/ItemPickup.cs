using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private void Awake()
    {
        Destroy(this.gameObject, 10);
    }

    public virtual void AddToCollection(ItemCollection collection)
    {
        Destroy(this.gameObject);
    }
}
