using UnityEngine;

public class VictoryActivator : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;
    [SerializeField]
    private RuntimeAnimatorController _animController;
    [SerializeField]
    private Transform _cameraTarget;
    [SerializeField]
    private Animator _victorScreenAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            
            var animator = other.GetComponent<Animator>();
            animator.runtimeAnimatorController = _animController;
            _particleSystem.Play(true);
            var cam = Camera.main.GetComponent<PlayerCamera>();
            cam.Clamp = false;
            cam.Target = _cameraTarget;

            other.GetComponent<PlayerAnimationEvents>().EventTriggered += VictoryActivator_EventTriggered;
        }
    }

    private void VictoryActivator_EventTriggered(PlayerAnimationEvents.EventType type)
    {
        if(type == PlayerAnimationEvents.EventType.FinishedVictory)
        {
            _victorScreenAnimator.enabled = true;
        }
    }
}
