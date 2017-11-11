using UnityEngine;

public class Pot : Damagable
{
    private Room _room;

    private bool _broken;
    private Renderer _renederer;
    private BoxCollider _boxCollider;

    private void Awake()
    {
        _renederer = GetComponent<Renderer>();
        _boxCollider = GetComponent<BoxCollider>();
        _room = GetComponentInParent<Room>();

        if (_room != null)
        {
            _room.PlayerLeft += delegate
            {
                Reset();
            };
        }
    }

    protected override void OnTookDamage(float baseDamage, GameObject damageCauser, DamageType damageType)
    {
        if (!_broken)
        {
            _broken = true;
            _renederer.enabled = false;
            _boxCollider.enabled = false;
            base.OnTookDamage(baseDamage, damageCauser, damageType);
        }
    }

    private void Reset()
    {
        if(_broken)
        {
            _broken = false;
            _renederer. enabled = true;
            _boxCollider.enabled = true;

        }
    }
}
