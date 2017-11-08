using UnityEngine;

public class SphereTraceComponent : LineTraceComponent
{
    [SerializeField]
    private float _radius = 0.5f;

    protected override int GetHitResults(Vector3 start, Vector3 end)
    {
        Vector3 direction = (end - start).normalized;
        return Physics.SphereCastNonAlloc(start, _radius, direction, _hitResults, direction.magnitude, _layerMask, _triggerInteraction);
    }

#if UNITY_EDITOR
    public override void DebugDrawTrace(Vector3 start, Vector3 end)
    {
        var pos = start + (end - start) * 0.5f;
        var rot = Quaternion.FromToRotation(Vector3.up, end - start);
        var height = Vector3.Distance(start, end) + _radius * 2;
        HandlesExtensions.DrawCapsule(pos, rot, height, _radius);
    }
#endif
}
