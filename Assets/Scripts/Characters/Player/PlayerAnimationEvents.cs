using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    public delegate void PlayerAnimationEvent(EventType type);
    public event PlayerAnimationEvent EventTriggered;

    private void TriggerStrike()
    {
        TriggerEvent(EventType.Strike);
    }

    private void TriggerEvent(EventType type)
    {
        if (EventTriggered != null)
            EventTriggered(type);
    }

	
    public enum EventType
    {
        Strike
    }
}
