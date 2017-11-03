using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<PlayerController>();
        _player.Struck += PlayerStruck;
    }

    private void PlayerStruck(object sender, System.EventArgs e)
    {
        _animator.SetTrigger("Strike");
    }
}
