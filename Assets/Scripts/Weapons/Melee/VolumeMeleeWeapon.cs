
using UnityEngine;

public class VolumeMeleeWeapon : MeleeWeapon
{
    private TriggerVolume _triggerVolume;


    protected override void Awake()
    {
        base.Awake();
        _triggerVolume = GetComponent<TriggerVolume>();
        if (_triggerVolume == null)
        {
            Debug.LogError("Could not find trigger volume.");
            enabled = false;
            return;
        }
    }

    protected override void Update()
    {
        base.Update();
        if(_attacking)
        {
            DamageObjectsInVolume();
        }
    }

    public override void AttackNow()
    {
        base.AttackNow();
        DamageObjectsInVolume();
    }

    private void DamageObjectsInVolume()
    {
        for (int i = 0; i < _triggerVolume.CollidersInVolume.Count; i++)
        {
            StrikeObjectIfAble(_triggerVolume.CollidersInVolume[i].gameObject, null);
        }
    }
}
