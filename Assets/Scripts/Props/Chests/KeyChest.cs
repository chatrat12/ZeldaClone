using UnityEngine;

public class KeyChest : Chest
{
    protected override void GiveItem()
    {
        base.GiveItem();
        var itemCollection = _characterThatOpened.GetComponent<ItemCollection>();
        if(itemCollection != null)
        {
            itemCollection.SilverKeyCount++;
        }
    }
}
