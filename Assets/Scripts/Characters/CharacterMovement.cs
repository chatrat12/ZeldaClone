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
    protected float _runSpeed = 10f;
    [SerializeField]
    protected float _knockbackTime = 0.5f;
    [SerializeField]
    protected bool _lookTowardsVelocity = true;

    protected Character _character;
    protected Rigidbody _rigidbody;

    protected float _timeKnockedBack = -1000f;
    protected Vector3 _knockbackDampeningVelocity = Vector3.zero;

    protected Vector3 _moveDirection = Vector3.zero;
    protected Quaternion _targetRotation;

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

    public virtual void Initialize(Character character)
    {
        _character = character;
        _rigidbody = character.GetComponent<Rigidbody>();
    }

    public void Knockback(Vector3 force)
    {
        _timeKnockedBack = Time.time;
        _rigidbody.velocity = force;
    }

    public virtual void Update()
    {
        if (_beingKnockedback)
        {
            _rigidbody.velocity = Vector3.SmoothDamp(_rigidbody.velocity, Vector3.zero, ref _knockbackDampeningVelocity, _knockbackTime);
        }
        else if (CanMove)
        {
            if (_moveDirection.magnitude >= 0.5f)
            {
                _xzVelocity = _moveDirection * _runSpeed;
                _targetRotation = Quaternion.LookRotation(_moveDirection.normalized);
            }
            else
                _xzVelocity = Vector3.zero;
        }
        _moveDirection = Vector3.zero;
    }

    public virtual void FixedUpdate()
    {
        Debug.Log(_rigidbody.velocity);

        if (!_beingKnockedback && CanMove && _lookTowardsVelocity)
        {
            _character.transform.rotation = _targetRotation;
        }
    }

    public virtual void Move(Vector3 direction)
    {
        _moveDirection = direction;
    }

    public enum MoveState
    {
        Grounded,
        Falling
    }
}
