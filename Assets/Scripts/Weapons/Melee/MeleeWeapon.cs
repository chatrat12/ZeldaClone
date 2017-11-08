using System.Collections.Generic;
using UnityEngine;

public abstract class MeleeWeapon : Weapon
{
    public float BaseDamage { get { return _baseDamage; } }

    [SerializeField]
    private float _baseDamage = 5f;
    [Tooltip("Decides if the weapon can apply damage to a single object multiple time in one strike window.")]
    [SerializeField]
    private bool _allowMultiHitOnSingleObject = false;
    [SerializeField]
    private float _multiHitCooldown = 0.2f;

    protected List<StrikeInfo> _strikeHistory = new List<StrikeInfo>();
    protected bool _attacking = false;

    public override void BeginAttack()
    {
        _strikeHistory.Clear();
        _attacking = true;
    }
    public override void EndAttack()
    {
        _attacking = false;
    }
    public override void AttackNow()
    {
        _strikeHistory.Clear();
    }
    protected bool StrikeObjectIfAble(GameObject obj, RaycastHit? hitInfo)
    {
        var result = CanStrike(obj);
        if (result)
        {
            _strikeHistory.Add(new StrikeInfo(obj, Time.time));
            DamageObject(obj, hitInfo);
        }
        return result;
    }
    protected virtual void DamageObject(GameObject obj, RaycastHit? hitInfo)
    {
        // TODO: Expose the rest of this.
        var direction = obj.transform.position - _owner.transform.position;
        Damage.ApplyPointDamage(obj, _baseDamage, _owner, direction, 600, hitInfo);
    }
    
    private bool CanStrike(GameObject obj)
    {
        if (obj == _owner)
            return false;
        foreach(var strike in _strikeHistory)
        {
            if(strike.ObjectStruck == obj)
            {
                if(!_allowMultiHitOnSingleObject || Time.time - strike.TimeStruck < _multiHitCooldown)
                {
                    return false;
                }
                else
                {
                    _strikeHistory.Remove(strike);
                    break;
                }
            }
        }
        return true;
    }

    protected struct StrikeInfo
    {
        public GameObject ObjectStruck { get; set; }
        public float TimeStruck { get; set; }

        public StrikeInfo(GameObject objectStruck, float timeStruck)
        {
            ObjectStruck = objectStruck;
            TimeStruck = timeStruck;
        }
    }
}
