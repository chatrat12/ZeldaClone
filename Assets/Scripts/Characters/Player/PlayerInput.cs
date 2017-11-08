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

        if(Input.GetKeyDown(KeyCode.Space))
        {
            _player.Strike();
        }

#if DEBUG
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Damage.ApplyGenericDamage(_player.gameObject, 2);
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            _player.Movement.Knockback(Vector3.forward * 10);
        }
#endif
    }

}
