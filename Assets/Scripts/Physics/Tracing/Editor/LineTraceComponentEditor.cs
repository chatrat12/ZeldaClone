using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LineTraceComponent), true)]
public class LineTraceComponentEditor : Editor
{
    private LineTraceComponent _tracer;

    private void LazyInitTracer()
    {
        if (_tracer == null)
            _tracer = (LineTraceComponent)target;
    }

    private void OnSceneGUI()
    {
        LazyInitTracer();
        DrawHandles(_tracer);
    }

    [DrawGizmo(GizmoType.Active)]
    private static void DrawGizmos(LineTraceComponent tracer, GizmoType gizmos)
    {
        DrawDebug(tracer);
        DrawHistory(tracer);
    }

    protected static void DrawHandles(LineTraceComponent tracer)
    {
        using (new Handles.DrawingScope(Color.green, tracer.transform.localToWorldMatrix))
        {
            EditorGUI.BeginChangeCheck();
            var size = HandleUtility.GetHandleSize(tracer.PointA) * 0.05f;
            var newPointA = Handles.FreeMoveHandle(tracer.PointA, Quaternion.identity, size, Vector3.zero, Handles.DotHandleCap);
            Handles.color = Color.red;
            size = HandleUtility.GetHandleSize(tracer.PointB) * 0.05f;
            var newPointB = Handles.FreeMoveHandle(tracer.PointB, Quaternion.identity, size, Vector3.zero, Handles.DotHandleCap);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(tracer, "Change Trace Point");
                tracer.PointA = newPointA;
                tracer.PointB = newPointB;
            }
        }
    }

    protected static void DrawDebug(LineTraceComponent tracer)
    {
        using (new Handles.DrawingScope(Color.red))
        {
            var matrix = tracer.transform.localToWorldMatrix;
            tracer.DebugDrawTrace(matrix.MultiplyPoint(tracer.PointA), 
                                  matrix.MultiplyPoint(tracer.PointB));
        }
    }
    protected static void DrawHistory(LineTraceComponent tracer)
    {
        using (new Handles.DrawingScope())
        {
            for (int i = 0; i < tracer.TraceHistory.Count; i++)
            {
                Handles.color = tracer.TraceHistory[i].SweepTrace ? Color.yellow : Color.red;
                tracer.DebugDrawTrace(tracer.TraceHistory[i].Start, tracer.TraceHistory[i].End);
            }
        }
    }
}
