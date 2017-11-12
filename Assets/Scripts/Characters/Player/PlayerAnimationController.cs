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
        _player.TookDamage += PlayerDamaged;
    }

    private void PlayerDamaged(float baseDamage, GameObject damageCauser, DamageType damageType)
    {
        if (_player.Health <= 0)
        {
            _animator.SetTrigger("Death");
            _animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
    }

    private void PlayerStruck(object sender, System.EventArgs e)
    {
        _animator.SetTrigger("Strike");
    }
}
