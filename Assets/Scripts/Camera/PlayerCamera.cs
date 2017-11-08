using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float _smoothTime = 0.2f;

    private Transform _target;
    private Vector3 _offset;
    private Camera _camera;

    private Vector3 _dampVelocity = Vector3.zero;

    private CameraVolume _currentVolume;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        var player = GetComponentInParent<PlayerController>();
        player.EnteredCameraVolume += Player_EnteredCameraVolume;

        _offset = transform.localPosition;
        _target = transform.parent;
        transform.SetParent(null);
    }

    private void Player_EnteredCameraVolume(CameraVolume cameraVolume)
    {
        _currentVolume = cameraVolume;
    }

    private void LateUpdate()
    {
        var targetPosition = _target.transform.position + _offset;
        if(_currentVolume != null)
            targetPosition =  _currentVolume.ConstrainTargetPosition(_camera, targetPosition);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _dampVelocity, _smoothTime);
        //transform.position = _target.transform.position + _offset;
    }
}
