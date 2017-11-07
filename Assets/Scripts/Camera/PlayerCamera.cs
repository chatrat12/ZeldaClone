using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float _smoothTime = 0.2f;

    private Transform _target;
    private Vector3 _offset;

    private Vector3 _dampVelocity = Vector3.zero;

    private void Awake()
    {
        _offset = transform.localPosition;
        _target = transform.parent;
        transform.SetParent(null);
    }

    private void LateUpdate()
    {
        var targetPosition = _target.transform.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _dampVelocity, _smoothTime);
        //transform.position = _target.transform.position + _offset;
    }
}
