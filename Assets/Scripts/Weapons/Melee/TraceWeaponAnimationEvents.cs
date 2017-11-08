using UnityEngine;

public class TraceWeaponAnimationEvents : MonoBehaviour
{
    private TracerMeleeWeapon _weapon;
    private PlayerAnimationEvents _events;

    private void Awake()
    {
        _weapon = GetComponent<TracerMeleeWeapon>();
        _events = GetComponentInParent<PlayerAnimationEvents>();
        _events.EventTriggered += _events_EventTriggered;
    }

    private void _events_EventTriggered(PlayerAnimationEvents.EventType type)
    {
        switch (type)
        {
            case PlayerAnimationEvents.EventType.OpenStrikeWindow:
                _weapon.BeginAttack();
                break;
            case PlayerAnimationEvents.EventType.CloseStrikeWindow:
                _weapon.EndAttack();
                break;
        }
    }
}
