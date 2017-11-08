using UnityEngine;

public abstract class TraceComponent : MonoBehaviour
{
    public delegate void TraceEvent(RaycastHit hitInfo);
    public event TraceEvent TraceHit;

    [SerializeField]
    protected LayerMask _layerMask;
    [SerializeField]
    protected QueryTriggerInteraction _triggerInteraction;

    protected Matrix4x4 _previousMatrix;

    public virtual void Trace() { } 
    protected void OnTraceHit(RaycastHit hitInfo)
    {
        if (TraceHit != null)
            TraceHit(hitInfo);
    }
    protected void UpdatePreviousMatrix()
    {
        _previousMatrix = transform.localToWorldMatrix;
    }

}
