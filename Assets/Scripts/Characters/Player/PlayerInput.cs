using UnityEngine;

[DefaultExecutionOrder(-10)]
public class PlayerInput : MonoBehaviour
{
    private PlayerController _player;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        _player.Movement.Move(new Vector3(h, 0, v).normalized);

        if (Input.GetButtonDown("Strike"))
        {
            _player.Strike();
        }
        if (Input.GetButtonDown("Interact"))
        {
            _player.Interact();
        }
        if (Input.GetButtonDown("Shoot"))
        {
            _player.FireArrow();
        }

#if DEBUG
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Damage.ApplyGenericDamage(_player.gameObject, 2);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            _player.Movement.AutoWalkTo(Vector3.forward * 5);
        }
#endif
    }

}
