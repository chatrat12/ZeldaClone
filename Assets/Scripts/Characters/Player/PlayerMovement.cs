using UnityEngine;

[System.Serializable]
public class PlayerMovement
{
    public float RunSpeed { get { return _runSpeed; } }
    public bool CanMove { get; set; }

    public Vector3 Velocity
    {
        get { return _rigidbody.velocity; }
        set { _rigidbody.velocity = value; }
    }

    [SerializeField]
    private float _runSpeed = 10f;
    [SerializeField]
    private float _knockbackTime = 0.5f;

    private PlayerController _controller;
    private Rigidbody _rigidbody;

    private float _timeKnockedBack = -1000f;
    private Vector3 _knockbackDampeningVelocity = Vector3.zero;

    private bool _beingKnockedback
    {
        get { return Time.time - _timeKnockedBack < _knockbackTime; }
    }

    private Vector3 _xzVelocity
    {
        get { return new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z); }
        set { _rigidbody.velocity = new Vector3(value.x, _rigidbody.velocity.y, value.z); }
    }

    public PlayerMovement()
    {
        CanMove = true;
    }

    public void Initialize(PlayerController controller)
    {
        _controller = controller;
        _rigidbody = controller.GetComponent<Rigidbody>();
    }

    public void Knockback(Vector3 force)
    {
        _timeKnockedBack = Time.time;
        _rigidbody.velocity = force;
    }

    public void Move(Vector3 direction)
    {
        if(_beingKnockedback)
        {
            _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, Vector3.zero, ref _knockbackDampeningVelocity, _knockbackTime);
        }
        else if (CanMove)
        {
            _xzVelocity = direction * _runSpeed;

            if (direction != Vector3.zero)
                _controller.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
