using UnityEngine;

public static class Damage
{
    private static Collider[] _overlapResults = new Collider[50];

    public static void ApplyGenericDamage(GameObject target, float baseDamage, GameObject damageCauser = null)
    {
        var damagable = target.GetComponent<Damagable>();
        if (damagable != null)
            damagable.TakeGenericDamage(baseDamage, damageCauser);
    }

    public static void ApplyPointDamage(GameObject target, float baseDamage, GameObject damageCauser = null, Vector3 hitDirection = default(Vector3), float force = 0, RaycastHit hitInfo = default(RaycastHit))
    {
        var damagable = target.GetComponent<Damagable>();
        if (damagable != null)
            damagable.TakePointDamage(baseDamage, damageCauser, hitDirection, force, hitInfo);
    }

    public static void ApplyRadialDamage(float baseDamage, GameObject damageCauser = null, Vector3 origin = default(Vector3), float radius = 0f, float force = 0f)
    {
        int numOfOverlaps = Physics.OverlapSphereNonAlloc(origin, radius, _overlapResults);
        if (numOfOverlaps > _overlapResults.Length)
            Debug.LogError("Number of results is larger than provided array!");
        Damagable damagable;
        for (int i = 0; i < numOfOverlaps; i++)
        {
            damagable = _overlapResults[i].gameObject.GetComponent<Damagable>();
            if (damagable != null)
                damagable.TakeRadialDamage(baseDamage, damageCauser, origin, radius, force);
        }
    }
}