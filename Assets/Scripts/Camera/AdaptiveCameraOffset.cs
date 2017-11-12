using UnityEngine;

[DefaultExecutionOrder(-10)]
public class AdaptiveCameraOffset : MonoBehaviour
{
    [SerializeField]
    private float _moveTime = 0.5f;
    [SerializeField]
    private float _lookAwayMoveTime = 0.1f;
    [SerializeField]
    private float _radius = 5f;
    

    private PlayerController _player;
    private Vector3 _offset = Vector3.zero;
    private Vector3 _dampVelocity;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    private void LateUpdate()
    {
        var direction = _player.Movement.PreviousMoveDirection;
        if(direction != Vector3.zero)
        {
            var unit = direction.normalized;
            var dot = Vector3.Dot(_offset.normalized, unit);

            var moveTime = dot >= 0 ? _moveTime : _lookAwayMoveTime;

            var target = unit * _radius;
            _offset = Vector3.SmoothDamp(_offset, target, ref _dampVelocity, moveTime * (1f / direction.magnitude));
            transform.position = transform.parent.position + _offset;
        }

        //_offset += _player.Movement.PreviousMoveDirection * _moveSpeed * Time.deltaTime;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(transform.position, 0.5f);
    }
#endif
}
