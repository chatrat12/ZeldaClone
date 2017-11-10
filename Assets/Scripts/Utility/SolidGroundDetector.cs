using UnityEngine;

[DefaultExecutionOrder(-100)]
public class SolidGroundDetector : MonoBehaviour
{
    public bool OnSolidGround { get; set; }

    [SerializeField]
    private float _radius = 1f;
    [SerializeField]
    private int _numberOfSensors = 8;
    [SerializeField]
    private float _distanceFromBase = 0.1f;
    [SerializeField]
    private float _raycastDistance = 0.2f;
    [SerializeField]
    private LayerMask _layersToCheckAgainst;

    private Vector3[] _sensorPoints;

#if UNITY_EDITOR
    private bool[] _sensorStatus;
#endif  

    private void Awake()
    {
        _sensorPoints = GetSensorPoints();
#if UNITY_EDITOR
        _sensorStatus = new bool[_sensorPoints.Length];
#endif
    }

    private void Update()
    {
        OnSolidGround = true;
        for (int i = 0; i < _sensorPoints.Length; i++)
        {
            var localPosition = _sensorPoints[i] + Vector3.up * _distanceFromBase;
            var position = transform.localToWorldMatrix.MultiplyPoint(localPosition);
            if (Physics.Raycast(position, Vector3.down, _raycastDistance, _layersToCheckAgainst.value, QueryTriggerInteraction.Ignore))
            {
#if UNITY_EDITOR
                _sensorStatus[i] = true;
#endif
            }
            else
            {
                OnSolidGround = false;
#if UNITY_EDITOR
                _sensorStatus[i] = false;

#endif
            }
        }
    }

    private Vector3[] GetSensorPoints()
    {
        var result = new Vector3[_numberOfSensors];

        var interval = Mathf.PI * 2 / _numberOfSensors;

        for (int i = 0; i < _numberOfSensors; i++)
        {
            var x = Mathf.Cos(interval * i);
            var z = Mathf.Sin(interval * i);
            result[i] = new Vector3(x, 0, z) * _radius;
        }
        return result;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (_sensorPoints == null) return;
        Gizmos.matrix = transform.localToWorldMatrix;
        for (int i = 0; i < _sensorPoints.Length; i++)
        {
            Gizmos.color = _sensorStatus[i] ? Color.green : Color.red;
            Gizmos.DrawSphere(_sensorPoints[i], 0.1f);
        }
        Gizmos.matrix = Matrix4x4.identity;
    }
#endif
}
