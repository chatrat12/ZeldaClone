using UnityEngine;

public class DoorwayBars : MonoBehaviour
{
    [SerializeField]
    private bool _startOpen = true;
    [SerializeField]
    private Mechanism _mechanism;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        if (_startOpen)
            _animator.SetTrigger("OpenOnStart");

        if(_mechanism != null)
        {
            _mechanism.Activated += delegate
            {
                Open();
            };
            _mechanism.Deactivated += delegate
            {
                Close();
            };
        }
    }

    public void Open()
    {
        _animator.SetTrigger("Open");
    }
    public void Close()
    {
        _animator.SetTrigger("Close");
    }

}
