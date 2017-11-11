﻿using UnityEngine;

[DefaultExecutionOrder(-10)]
public class PlayerCamera : MonoBehaviour
{
    public Transform Target { get; set; }


    [SerializeField]
    private float _smoothTime = 0.2f;

    private Camera _camera;
    private Vector3 _dampVelocity = Vector3.zero;
    private CameraVolume _currentVolume;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        var player = GetComponentInParent<PlayerController>();
        player.EnteredCameraVolume += Player_EnteredCameraVolume;
        player.LeftCameraVolume += Player_LeftCameraVolume;
    }

    
    private void Player_EnteredCameraVolume(CameraVolume cameraVolume)
    {
        _currentVolume = cameraVolume;
    }
    private void Player_LeftCameraVolume(CameraVolume cameraVolume)
    {
        //_currentVolume = null;
    }


    private void LateUpdate()
    {
        var targetPosition = Target.transform.position;
        if(_currentVolume != null)
            targetPosition =  _currentVolume.ConstrainTargetPosition(_camera, targetPosition);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _dampVelocity, _smoothTime, float.MaxValue, Time.unscaledDeltaTime);
    }
}
