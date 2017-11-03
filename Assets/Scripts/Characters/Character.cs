using UnityEngine;

public class Character : Damagable
{
    public int MaxHealth = 100;
    public int Health { get; protected set; }
    public float InvincibleTimeframe = 1f;
    protected float _lastTimeDamaged = -1000f;

    public bool CanDamage { get { return Time.time - _lastTimeDamaged > InvincibleTimeframe; } }
    public bool IsAlive { get { return Health > 0; } }


    protected virtual void Awake()
    {
        Health = MaxHealth;
    }
    protected virtual void Update() { }
    protected virtual void FixedUpdate() { }
    protected virtual void OnDestroy()
    {
    }
    protected void Kill()
    {
        OnDeath();
    }
    protected virtual void OnDeath()
    {
        Destroy(this.gameObject);

    }

    protected override void OnTookDamage(float baseDamage, GameObject damageCauser, DamageType damageType)
    {
        if(baseDamage > 0) // Not being healed
        {
            if (!CanDamage || !IsAlive) return; // If can't be damaged, return
            _lastTimeDamaged = Time.time;
        }
        Health -= Mathf.RoundToInt(baseDamage);
        Health = Mathf.Clamp(Health, 0, MaxHealth);
        if (Health <= 0)
            Kill();
        base.OnTookDamage(baseDamage, damageCauser, damageType);
    }
}
