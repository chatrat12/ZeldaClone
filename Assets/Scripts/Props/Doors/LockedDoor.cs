using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    private Animator _animator;

    protected virtual void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    protected void Open()
    {
        _animator.SetTrigger("Open");
    }
}
