using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(200)]
public class LineTraceComponent : TraceComponent
{
    private const int RESULT_SIZE = 10;

    [SerializeField]
    protected Vector3 _pointA = Vector3.down;
    [SerializeField]
    protected Vector3 _pointB = Vector3.up;
    [SerializeField]
    private bool _sweep = true;
    [SerializeField]
    private float _sweepDistance = 0.1f;

    public Vector3 PointA { get { return _pointA; } set { _pointA = value; } }
    public Vector3 PointB { get { return _pointB; } set { _pointB = value; } }

    protected Vector3 _pointAWorld { get { return transform.localToWorldMatrix.MultiplyPoint(PointA); } }
    protected Vector3 _pointBWorld { get { return transform.localToWorldMatrix.MultiplyPoint(PointB); } }

    protected RaycastHit[] _hitResults = new RaycastHit[RESULT_SIZE];

#if UNITY_EDITOR
    public List<LineTraceHistory> TraceHistory { get { return _traceHistory; } }
    private List<LineTraceHistory> _traceHistory = new List<LineTraceHistory>();
#endif
    protected virtual void Awake()
    {
        UpdatePreviousMatrix();
    }
    protected virtual void Update()
    {
        UpdatePreviousMatrix();
    }

    public override void Trace()
    {
        Trace(transform.localToWorldMatrix, false);

        if (_sweep)
        {
            var curPointA = transform.localToWorldMatrix.MultiplyPoint(PointA);
            var curPointB = transform.localToWorldMatrix.MultiplyPoint(PointB);
            var prevPointA = _previousMatrix.MultiplyPoint(PointA);
            var prevPointB = _previousMatrix.MultiplyPoint(PointB);

            var maxDist = Mathf.Max(Vector3.Distance(curPointA, prevPointA),
                                    Vector3.Distance(curPointB, prevPointB));
            if(maxDist > _sweepDistance)
            {
                var sweepCount = Mathf.FloorToInt(maxDist / _sweepDistance);
                var interval = 1f / (sweepCount + 1);
                for (int i = 0; i < sweepCount; i++)
                {
                    var alpha = interval * (i + 1);
                    var sweepA = Vector3.Lerp(curPointA, prevPointA, alpha);
                    var sweepB = Vector3.Lerp(curPointB, prevPointB, alpha);
                    Trace(sweepA, sweepB, true);
                }
            }
        }
    }

    protected void Trace(Matrix4x4 matrix, bool isSweep)
    {
        Trace(matrix.MultiplyPoint(PointA), matrix.MultiplyPoint(PointB), isSweep);
    }
    protected void Trace(Vector3 start, Vector3 end, bool isSweep)
    {
        var numberOfHits = GetHitResults(start, end);
        for (int i = 0; i < numberOfHits; i++)
            OnTraceHit(_hitResults[i]);
#if UNITY_EDITOR
        AddTraceToHistory(start, end, isSweep);
#endif
    }

    protected virtual int GetHitResults(Vector3 start, Vector3 end)
    {
        return PhysicsExtensions.LinecastAllNonAlloc(start, end, _hitResults, _layerMask, _triggerInteraction);
    }


#if UNITY_EDITOR
    public virtual void DebugDrawTrace(Vector3 start, Vector3 end)
    {
        UnityEditor.Handles.DrawLine(start, end);
    }

    private void AddTraceToHistory(Vector3 start, Vector3 end, bool sweepTrace)
    {
        _traceHistory.Add(new LineTraceHistory(start, end, sweepTrace));
        if (_traceHistory.Count > 50)
            _traceHistory.RemoveAt(0);
    }


    public struct LineTraceHistory
    {
        public Vector3 Start { get; set; }
        public Vector3 End { get; set; }
        public bool SweepTrace { get; set; }
        public LineTraceHistory(Vector3 start, Vector3 end, bool sweepTrace)
        {
            Start = start;
            End = end;
            SweepTrace = sweepTrace;
        }
        public override string ToString()
        {
            return Start.ToString();
        }
    }
#endif
}
