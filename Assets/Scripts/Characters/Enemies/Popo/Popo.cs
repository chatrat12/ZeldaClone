using UnityEngine;

public class Popo : Enemy
{

    [SerializeField]
    [MinMaxLimit(0, 5)]
    private MinMaxFloat _chaseTime = new MinMaxFloat(1.0f, 2.0f);
    [SerializeField]
    [MinMaxLimit(0, 5)]
    private MinMaxFloat _idleTime = new MinMaxFloat(0.5f, 1.0f);

    [SerializeField]
    private CharacterMovement _movement;

    private GameObject _target;

    private bool _chasing = true;
    private float _toggleChaseTime = 0;

    protected override void Awake()
    {
        base.Awake();
        _movement.Initialize(this);
        _target = FindObjectOfType<PlayerController>().gameObject;
        _toggleChaseTime = Time.time + _chaseTime.GetRandom();
    }

    protected override void Update()
    {
        if (IsAlive)
        {
            if (Time.time >= _toggleChaseTime)
            {
                _chasing = !_chasing;
                _toggleChaseTime = Time.time + (_chasing ? _chaseTime.GetRandom() : _idleTime.GetRandom());
            }
            if (_chasing)
                ChaseTarget();
        }
        _movement.Update();
        base.Update();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _chasing = true;
        _toggleChaseTime = Time.time + _chaseTime.GetRandom();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        _movement.Velocity = Vector3.zero;
        WarpToSpawn();
    }

    private void ChaseTarget()
    {
        var direction = _target.transform.position - transform.position;
        direction.y = 0;
        direction.Normalize();
        _movement.Move(direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.collider.GetComponent<PlayerController>();
        if(player != null)
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();
            Damage.ApplyGenericDamage(player.gameObject, 2, this.gameObject);
            player.Movement.Knockback(direction * 15);
            _movement.Knockback(-direction * 15);
        }
    }

    protected override void OnTookDamage(float baseDamage, GameObject damageCauser, DamageType damageType)
    {
        if (!enabled)
            return;
        if(damageCauser != null)
        {
            var direction = transform.position - _target.transform.position;
            direction.y = 0;
            direction.Normalize();
            _movement.Knockback(direction * 25);

        }
        base.OnTookDamage(baseDamage, damageCauser, damageType);
    }

    protected override void OnDeath()
    {
        Destroy(this.gameObject, 0.3f);
    }

}
