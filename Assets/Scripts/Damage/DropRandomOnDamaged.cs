using UnityEngine;

public class DropRandomOnDamaged : MonoBehaviour
{

    [SerializeField]
    private GameObject[] _objectsToDrop;

    private void Awake()
    {
        GetComponent<Damagable>().TookDamage += DropOnDamaged_TookDamage;
    }

    private void DropOnDamaged_TookDamage(float baseDamage, GameObject damageCauser, DamageType damageType)
    {
        if (baseDamage > 0f)
        {
            var objectToDrop = _objectsToDrop[Random.Range(0, _objectsToDrop.Length)];
            Instantiate(objectToDrop, transform.position, transform.rotation);
        }
    }
}
