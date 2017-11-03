using UnityEngine;

public class Destructable : Damagable
{
    [SerializeField]
    private float _damageThreshold = 0f;

    protected override void OnTookDamage(float baseDamage, GameObject damageCauser, DamageType damageType)
    {
        base.OnTookDamage(baseDamage, damageCauser, damageType);
        if (baseDamage >= _damageThreshold)
            Destroy(this.gameObject);
    }

}
