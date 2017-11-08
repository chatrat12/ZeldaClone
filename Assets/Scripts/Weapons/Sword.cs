using UnityEngine;

[RequireComponent(typeof(TriggerVolume))]
public class Sword : MonoBehaviour
{
    [SerializeField]
    private float _damage = 1f;
    
    private TriggerVolume _triggerVolume;
    private Character _owner;

    private void Awake()
    {
        _owner = GetComponentInParent<Character>();
        _triggerVolume = GetComponent<TriggerVolume>();
    }

    public void StrikeObjectsInVolume()
    {
        foreach(var collider in _triggerVolume.CollidersInVolume)
        {
            Damage.ApplyGenericDamage(collider.gameObject, _damage, _owner.gameObject);
        }
    }
}
