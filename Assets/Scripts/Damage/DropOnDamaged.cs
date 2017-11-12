using UnityEngine;

public class DropOnDamaged : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectToDrop;

    private void Awake()
    {
        GetComponent<Damagable>().TookDamage += DropOnDamaged_TookDamage;
    }

    private void DropOnDamaged_TookDamage(float baseDamage, GameObject damageCauser, DamageType damageType)
    {
        if(baseDamage > 0f)
        {
            var newObject = Instantiate(_objectToDrop, transform.position, transform.rotation);
            Destroy(newObject, 10);
        }
    }
}
