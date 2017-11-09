using UnityEngine;

public class LockedDoor : InteractableObject
{
    private Animator _animator;

    protected virtual void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public override void Interact(Character character)
    {
        var itemCollection = character.GetComponent<ItemCollection>();
        if(itemCollection != null && itemCollection.SilverKeyCount > 0)
        {
            itemCollection.SilverKeyCount--;
            Open();
        }
    }

    protected void Open()
    {
        _animator.SetTrigger("Open");
    }
}
