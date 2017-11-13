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

    private Vector3 _dampVelocity = Vector3.zero;
    private Transform _modelTransform;

    protected override void Awake()
    {
        base.Awake();
        _modelTransform = transform.GetChild(0);
    }

    protected override void Update()
    {
        ApplyJumpCurve();
        MoveTowardsTarget();
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

        var translation = direction * speed * Time.deltaTime;

        if (translation.magnitude > targetVector.magnitude) // Prevent overshoot jitter.
            transform.position = TargetTransform.position;
        else
            transform.position += translation;
    }

    protected override void OnTookDamage(float baseDamage, GameObject damageCauser, DamageType damageType)
    {
        Debug.Log("TookDamage");
        base.OnTookDamage(baseDamage, damageCauser, damageType);
    }
    protected override void OnTookPointDamage(float baseDamage, GameObject damageCauser, Vector3 hitDirection, float force, RaycastHit? hitInfo)
    {
        base.OnTookPointDamage(baseDamage, damageCauser, hitDirection, force, hitInfo);
    }

}
