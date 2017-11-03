class HeartPickup : ItemPickup
{
    public override void AddToCollection(ItemCollection collection)
    {
        var character = collection.GetComponent<Character>();
        if(character != null)
        {
            base.AddToCollection(collection);
            Damage.ApplyGenericDamage(character.gameObject, -4);
        }
    }
}
