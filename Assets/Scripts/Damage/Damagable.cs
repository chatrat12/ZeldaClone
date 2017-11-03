

using UnityEngine;

public class Damagable : MonoBehaviour
{
    public delegate void TakeDamageEvent(float baseDamage, GameObject damageCauser, DamageType damageType);
    public delegate void TakePointDamageEvent(float baseDamage, GameObject damageCauser, Vector3 hitDirection, float force, RaycastHit hitInfo);
    public delegate void TakeRadialDamageEvent(float baseDamage, GameObject damageCauser, Vector3 origin, float radius, float force);

    public event TakeDamageEvent TookDamage;
    public event TakePointDamageEvent TookPointDamage;
    public event TakeRadialDamageEvent TookRadialDamage;


    public void TakeGenericDamage(float baseDamage, GameObject damageCauser = null)
    {
        OnTookDamage(baseDamage, damageCauser, DamageType.Generic);
    }

    public void TakePointDamage(float baseDamage, GameObject damageCauser = null, Vector3 hitDirection = default(Vector3), float force = 0f, RaycastHit hitInfo = default(RaycastHit))
    {
        OnTookPointDamage(baseDamage, damageCauser, hitDirection, force, hitInfo);
    }

    public void TakeRadialDamage(float baseDamage, GameObject damageCauser = null, Vector3 origin = default(Vector3), float radius = 0f, float force = 0f)
    {
        OnTookRadialDamage(baseDamage, damageCauser, origin, radius, force);
    }

    protected virtual void OnTookDamage(float baseDamage, GameObject damageCauser, DamageType damageType)
    {
        if (TookDamage != null)
            TookDamage(baseDamage, damageCauser, DamageType.Generic);
    }
    protected virtual void OnTookPointDamage(float baseDamage, GameObject damageCauser, Vector3 hitDirection, float force, RaycastHit hitInfo)
    {

        OnTookDamage(baseDamage, damageCauser, DamageType.Point);
        if (TookPointDamage != null)
            TookPointDamage(baseDamage, damageCauser, hitDirection, force, hitInfo);
    }
    protected virtual void OnTookRadialDamage(float baseDamage, GameObject damageCauser, Vector3 origin, float radius, float force)
    {
        OnTookDamage(baseDamage, damageCauser, DamageType.Point);
        if (TookRadialDamage != null)
            TookRadialDamage(baseDamage, damageCauser, origin, radius, force);
    }
}

public enum DamageType
{
    Generic,
    Point,
    Radial
}
