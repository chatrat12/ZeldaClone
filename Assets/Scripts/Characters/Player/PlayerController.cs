using UnityEngine;

public class PlayerController : Character
{
    public event System.EventHandler Struck;

    [SerializeField]
    private PlayerMovement _movement;

    public PlayerMovement Movement { get { return _movement; } }

    private Sword _sword;

    protected override void Awake()
    {
        base.Awake();
        _movement.Initialize(this);

        _sword = GetComponentInChildren<Sword>();
        GetComponent<PlayerAnimationEvents>().EventTriggered += PlayerController_EventTriggered;
    }

    private void PlayerController_EventTriggered(PlayerAnimationEvents.EventType type)
    {
        switch(type)
        {
            case PlayerAnimationEvents.EventType.Strike:
                _sword.StrikeObjectsInVolume();
                break;
        }
    }

    protected override void OnDeath()
    {
        Debug.Log("you dead!");
    }

    public void Strike()
    {
        if (Struck != null)
            Struck(this, null);
    }
}
