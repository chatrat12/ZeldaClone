using UnityEngine;

public class ArmosKnightsRoutine : MonoBehaviour
{
    public float KnightMoveSpeed { get { return _knightMoveSpeed; } }
    public AnimationCurve KnightJumpCurve { get { return _knightJumpCurve; } }
    public float MaxKnightJumpHeight { get { return _maxKnightJumpHeight; } }

    [SerializeField]
    private int _numberOfKnights = 6;

    [SerializeField]
    private float _outerRadius = 10f;
    [SerializeField]
    private float _innerRadius = 5f;

    [SerializeField]
    private float _timeAtOuterRadius = 1;
    [SerializeField]
    private float _timeAtInnerRadius = 1;
    [SerializeField]
    private float _timeWaitingAtLine = 1;
    [SerializeField]
    private float _timeAdvancingLine = 1;

    [SerializeField]
    [Range(0, 0.5f)]
    private float _ringSpinSpeed = 1f;
    [SerializeField]
    private float _knightMoveSpeed = 5f;
    [SerializeField]
    private AnimationCurve _knightJumpCurve = new AnimationCurve()
    {
        keys = new Keyframe[]
        {
            new Keyframe(0, 0),
            new Keyframe(0.5f, 1),
            new Keyframe(1, 0)
        }
    };


    [SerializeField]
    private ArmosKnight _knightPrefab;

    private Transform[] _targets;

    private RoutineState _state = RoutineState.OuterRadius;
    private float _radialOffset;
    private float _interval;
    private float _advanceRoutineTime;
    private float _maxKnightJumpHeight;

    private void Awake()
    {
        _maxKnightJumpHeight = GetMaxJumpHeight();
        _interval = Mathf.PI * 2 / _numberOfKnights;
        _advanceRoutineTime = Time.time + _timeAtOuterRadius;
        CreateTargets();
        DoCircleFormation(_outerRadius);
        SpawnKnights();
    }

    private void Update()
    {
        if (Time.time >= _advanceRoutineTime)
            AdvanceRoutine();
        switch(_state)
        {
            case RoutineState.OuterRadius:
                DoCircleFormation(_outerRadius);
                break;
            case RoutineState.InnerRadius:
                DoCircleFormation(_innerRadius);
                break;
            case RoutineState.WaitingAtLine:
                LineUp(_outerRadius);
                break;
            case RoutineState.AdvancingLine:
                LineUp(-_outerRadius);
                break;

        }
    }

    private void CreateTargets()
    {
        _targets = new Transform[_numberOfKnights];
        for (int i = 0; i < _numberOfKnights; i++)
        {
            var newGO = new GameObject("Armos Knight Target " + i);
            newGO.transform.SetParent(this.transform);
            newGO.transform.localPosition = Vector3.zero;
            _targets[i] = newGO.transform;
        }
    }
    private void SpawnKnights()
    {
        for (int i = 0; i < _numberOfKnights; i++)
        {
            var knight = Instantiate(_knightPrefab, this.transform);
            knight.gameObject.name = "ArmosKnight_" + i;
            knight.TargetTransform = _targets[i];
            knight.Routine = this;
            knight.transform.position = knight.TargetTransform.position;
        }
    }

    private void DoCircleFormation(float radius)
    {
        _radialOffset += Mathf.PI * 2 * _ringSpinSpeed * Time.deltaTime;
        for (int i = 0; i < _numberOfKnights; i++)
        {
            var x = Mathf.Cos(_interval * i + _radialOffset);
            var z = Mathf.Sin(_interval * i + _radialOffset);
            _targets[i].localPosition = new Vector3(x, 0, z) * radius;
        }
    }
    private void LineUp(float z)
    {
        var interval = _outerRadius * 2 / (_numberOfKnights - 1);
        for (int i = 0; i < _numberOfKnights; i++)
        {
            var x = -_outerRadius + interval * i;
            _targets[i].transform.localPosition = new Vector3(x, 0, z);
        }
    }

    private void AdvanceRoutine()
    {
        switch(_state)
        {
            case RoutineState.OuterRadius:
                _state = RoutineState.InnerRadius;
                _advanceRoutineTime = Time.time + _timeAtInnerRadius;
                break;
            case RoutineState.InnerRadius:
                _state = RoutineState.WaitingAtLine;
                _advanceRoutineTime = Time.time + _timeWaitingAtLine;
                break;
            case RoutineState.WaitingAtLine:
                _state = RoutineState.AdvancingLine;
                _advanceRoutineTime = Time.time + _timeAdvancingLine;
                break;
            case RoutineState.AdvancingLine:
                _state = RoutineState.OuterRadius;
                _advanceRoutineTime = Time.time + _timeAtOuterRadius;
                break;
        }
    }

    private float GetMaxJumpHeight()
    {
        float result = 0;
        foreach(var key in _knightJumpCurve.keys)
        {
            if (key.value > result)
                result = key.value;
        }
        return result;
    }

    enum RoutineState
    {
        OuterRadius,
        InnerRadius,
        WaitingAtLine,
        AdvancingLine
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (_targets == null) return;
        foreach(var target in _targets)
        {
            Gizmos.DrawSphere(target.position, 0.25f);
        }
    }
#endif

}
