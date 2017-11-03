using UnityEngine;

[System.Serializable]
public class PlayerMovement
{
    public float RunSpeed { get { return _runSpeed; } }
    public bool CanMove { get; set; }

    [SerializeField]
    private float _runSpeed = 10f;

    private PlayerController _controller;
    private Rigidbody _rigidbody;

    public PlayerMovement()
    {
        CanMove = true;
    }

    public void Initialize(PlayerController controller)
    {
        _controller = controller;
        _rigidbody = controller.GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        if (CanMove)
        {
            _rigidbody.velocity = direction * _runSpeed;
            if (direction != Vector3.zero)
                _controller.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

}
