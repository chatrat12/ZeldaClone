using UnityEngine;

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
    }

}
