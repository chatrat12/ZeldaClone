using UnityEngine;

[DefaultExecutionOrder(-10)]
public class PlayerInput : MonoBehaviour
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";
    private const string STRIKE = "Strike";
    private const string INTERACT = "Interact";
    private const string SHOOT = "Shoot";
    private const string PAUSE = "Pause";

    private PlayerController _player;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (_player.GamePaused) return;
        var h = Input.GetAxis(HORIZONTAL_AXIS);
        var v = Input.GetAxis(VERTICAL_AXIS);
        var direction = new Vector3(h, 0, v);
        if (direction.magnitude > 1)
            direction.Normalize();
        _player.Movement.Move(direction);

        if (Input.GetButtonDown(STRIKE))
            _player.Strike();
        if (Input.GetButtonDown(INTERACT))
            _player.Interact();
        if (Input.GetButtonDown(SHOOT))
            _player.FireArrow();
        if (Input.GetButtonDown(PAUSE))
            _player.Pause();

#if DEBUG
        if (Input.GetKeyDown(KeyCode.Q))
            Damage.ApplyGenericDamage(_player.gameObject, 20);
#endif
    }

}
