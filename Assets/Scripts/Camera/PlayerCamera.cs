using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform _target;
    private Vector3 _offset;

    private void Awake()
    {
        _offset = transform.localPosition;
        _target = transform.parent;
        transform.SetParent(null);
    }

    private void LateUpdate()
    {
        transform.position = _target.transform.position + _offset;
    }
}
