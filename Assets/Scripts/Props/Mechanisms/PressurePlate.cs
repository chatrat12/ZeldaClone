using UnityEngine;

public class PressurePlate : Mechanism
{
    private Animator _animator;

    [SerializeField]
    private Color _activatedColor;

    bool _activated = false;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_activated) return;
        if (!_activated && other.GetComponent<PlayerController>())
        {
            GetComponentInChildren<Renderer>().material.color = _activatedColor;
            _animator.SetTrigger("Press");
            _activated = true;
            OnActivate();
        }
    }
}
