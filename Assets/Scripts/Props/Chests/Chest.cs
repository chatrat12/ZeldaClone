using UnityEngine;

public class Chest : InteractableObject
{
    private Animator _animator;
    protected Character _characterThatOpened;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public override void Interact(Character character)
    {
        _animator.SetTrigger("Open");
        _characterThatOpened = character;
    }

    protected virtual void GiveItem()
    {
        RemoveRevealedItem();
    }

    protected void RemoveRevealedItem()
    {
        // Item revealer should be the first child of the chest!
        Destroy(transform.GetChild(0).gameObject);
    }
}
