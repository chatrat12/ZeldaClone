
using UnityEngine;

public class TracerMeleeWeapon : MeleeWeapon
{
    private TraceComponent _tracer;

    protected override void Awake()
    {
        base.Awake();
        _tracer = GetComponent<TraceComponent>();
        if (_tracer == null)
        {
            Debug.LogError("Could not find tracer.");
            enabled = false;
            return;
        }
        _tracer.TraceHit += OnTraceHit;
    }

    protected override void Update()
    {
        base.Update();
        if (_attacking)
        {
            _tracer.Trace();
        }
    }

    public override void AttackNow()
    {
        base.AttackNow();
        _tracer.Trace();
    }

    private void OnTraceHit(RaycastHit hitInfo)
    {
        var obj = hitInfo.collider.gameObject;
        StrikeObjectIfAble(obj, hitInfo);
    }
}
