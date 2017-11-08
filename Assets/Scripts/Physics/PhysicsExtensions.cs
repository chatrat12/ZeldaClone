using UnityEngine;

public static class PhysicsExtensions
{
    public static RaycastHit[] LinecastAll(Vector3 start, Vector3 end, int layerMask = -5, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
    {
        Vector3 direction = end - start;
        return Physics.RaycastAll(start, direction, direction.magnitude, layerMask, queryTriggerInteraction);
    }
    public static int LinecastAllNonAlloc(Vector3 start, Vector3 end, RaycastHit[] results, int layerMask = -5, QueryTriggerInteraction queryTriggerInteraction = QueryTriggerInteraction.UseGlobal)
    {
        Vector3 direction = end - start;
        return Physics.RaycastNonAlloc(start, direction, results, direction.magnitude, layerMask, queryTriggerInteraction);
    }
}

