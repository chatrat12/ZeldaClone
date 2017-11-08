using UnityEngine;

public class PlayerController : Character
{
    public delegate void CameraVolumeEvent(CameraVolume cameraVolume);
    public event CameraVolumeEvent EnteredCameraVolume;
    public event CameraVolumeEvent LeftCameraVolume;

    public event System.EventHandler Struck;

    [SerializeField]
    private PlayerMovement _movement;

    public PlayerMovement Movement { get { return _movement; } }

    private Sword _sword;
    private Vector3 _lastGroundedPosition;
    private SolidGroundDetector _groundDetector;

    protected override void Awake()
    {
        base.Awake();
        _movement.Initialize(this);

        _sword = GetComponentInChildren<Sword>();
        GetComponent<PlayerAnimationEvents>().EventTriggered += PlayerController_EventTriggered;

        _lastGroundedPosition = transform.position;
        _groundDetector = GetComponent<SolidGroundDetector>();
    }
    protected override void Update()
    {
        if (_groundDetector.OnSolidGround)
            _lastGroundedPosition = transform.position;
        _movement.Update();
        base.Update();
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

    public void WarpToLastGroundedPosition()
    {
        transform.position = _lastGroundedPosition;
        Movement.Velocity = Vector3.zero;
    }

    public void Strike()
    {
        if (Struck != null)
            Struck(this, null);
    }

    private void OnTriggerEnter(Collider other)
    {
        var cameraVolume = other.GetComponent<CameraVolume>();
        if(cameraVolume != null)
        {
            if (EnteredCameraVolume != null)
                EnteredCameraVolume(cameraVolume);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        var cameraVolume = other.GetComponent<CameraVolume>();
        if (cameraVolume != null)
        {
            if (LeftCameraVolume != null)
                LeftCameraVolume(cameraVolume);
        }
    }
}
