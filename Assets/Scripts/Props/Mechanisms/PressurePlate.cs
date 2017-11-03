using UnityEngine;

public class PressurePlate : Mechanism
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _animator.SetTrigger("Press");
        OnActivate();
    }
}
