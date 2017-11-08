using UnityEngine;

[System.Serializable]
public class CharacterMovement
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
    [SerializeField]
    private bool _lookTowardsVelocity = true;

    private Character _character;
    private Rigidbody _rigidbody;

    private float _timeKnockedBack = -1000f;
    private Vector3 _knockbackDampeningVelocity = Vector3.zero;

    private Vector3 _moveDirection = Vector3.zero;

    private bool _beingKnockedback
    {
        get { return Time.time - _timeKnockedBack < _knockbackTime; }
    }

    private Vector3 _xzVelocity
    {
        get { return new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z); }
        set { _rigidbody.velocity = new Vector3(value.x, _rigidbody.velocity.y, value.z); }
    }

    public CharacterMovement()
    {
        CanMove = true;
    }

    public void Initialize(Character character)
    {
        _character = character;
        _rigidbody = character.GetComponent<Rigidbody>();
    }

    public void Knockback(Vector3 force)
    {
        _timeKnockedBack = Time.time;
        _rigidbody.velocity = force;
    }

    public void Update()
    {
        if (_beingKnockedback)
        {
            _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, Vector3.zero, ref _knockbackDampeningVelocity, _knockbackTime);
        }
        else if (CanMove)
        {
            _xzVelocity = _moveDirection * _runSpeed;

            if (_lookTowardsVelocity)
            {
                if (_moveDirection != Vector3.zero)
                    _character.transform.rotation = Quaternion.LookRotation(_moveDirection);
            }
        }
        _moveDirection = Vector3.zero;
    }

    public void Move(Vector3 direction)
    {
        _moveDirection = direction;
    }

    public enum MoveState
    {
        Grounded,
        Falling
    }
}
