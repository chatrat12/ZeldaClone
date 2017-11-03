using UnityEngine;

public class KeyPickup : ItemPickup
{
    public override void AddToCollection(ItemCollection collection)
    {
        base.AddToCollection(collection);
        collection.SilverKeyCount++;
    }
}
