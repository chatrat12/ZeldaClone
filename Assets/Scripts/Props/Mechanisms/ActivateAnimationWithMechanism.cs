using UnityEngine;

public class ActivateAnimationWithMechanism : MonoBehaviour
{
    [SerializeField]
    private Mechanism _mechanism;

    private Animator _animator;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mechanism.Activated += delegate
        {
            _animator.SetTrigger("Activate");
        };
    }
}
