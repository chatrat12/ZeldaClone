using UnityEngine;

public class ArmosKnight : Enemy
{
    public Transform TargetTransform { get; set; }
    public ArmosKnightsRoutine Routine { get; set; }

    private AnimationCurve _jumpCurve { get { return Routine.KnightJumpCurve; } }
    private float _jumpDuration { get { return _jumpCurve.keys[_jumpCurve.length - 1].time; } }
    private float _maxJumpHeight { get { return Routine.MaxKnightJumpHeight; } }
    private float _height
    {
        get { return _modelTransform.localPosition.y; }
        set { _modelTransform.localPosition = Vector3.up * value; }
    }

    private Rigidbody _rigidbody;
    private Vector3 _dampVelocity = Vector3.zero;
    private Transform _modelTransform;
    private KnockbackController _knockback = new KnockbackController();


    protected override void Awake()
    {
        base.Awake();
        _modelTransform = transform.GetChild(0);
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        base.Update();
        ApplyJumpCurve();
        if (_knockback.BeingKnockedback)
            _rigidbody.velocity = _knockback.GetVelocity(_rigidbody.velocity);
        else
            MoveTowardsTarget();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();

    }

    private void ApplyJumpCurve()
    {
        _height = _jumpCurve.Evaluate(Time.time % _jumpDuration);
    }
    private void MoveTowardsTarget()
    {
        var targetVector = TargetTransform.position - transform.position;
        var direction = targetVector;
        direction.y = 0;
        direction.Normalize();
        var speed = _height / _maxJumpHeight * Routine.KnightMoveSpeed;

        var translation = direction * speed * Time.fixedDeltaTime;

        if (translation.magnitude > targetVector.magnitude) // Prevent overshoot jitter.
            _rigidbody.position = TargetTransform.position;
        else
        {
            _rigidbody.velocity = direction * speed;
        }
    }

    protected override void OnTookPointDamage(float baseDamage, GameObject damageCauser, Vector3 hitDirection, float force, RaycastHit? hitInfo)
    {
        if (CanDamage)
        {
            _rigidbody.velocity = hitDirection * force;
            _knockback.Knockback(InvincibleTimeframe);
        }
        base.OnTookPointDamage(baseDamage, damageCauser, hitDirection, force, hitInfo);
    }

}
