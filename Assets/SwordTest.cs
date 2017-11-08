using UnityEngine;

public class SwordTest : MonoBehaviour
{
    private SphereTraceComponent _tracer;

    private void Awake()
    {
        _tracer = GetComponent<SphereTraceComponent>();
        
    }

    private void Update()
    {
        _tracer.Trace();
    }

}
