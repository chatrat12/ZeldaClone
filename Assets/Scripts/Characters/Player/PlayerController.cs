using UnityEngine;

public class PlayerController : Character
{

    public delegate void CameraVolumeEvent(CameraVolume cameraVolume);
    public event CameraVolumeEvent EnteredCameraVolume;
    public event CameraVolumeEvent LeftCameraVolume;

    public delegate void PlayerEvent(PlayerController sender);
    public event PlayerEvent RequestedGamePause;

    public event System.EventHandler Struck;

    [SerializeField]
    private PlayerMovement _movement;

    public PlayerMovement Movement { get { return _movement; } }
    public bool GamePaused { get; set; }

    private Vector3 _lastGroundedPosition;
    private SolidGroundDetector _groundDetector;
    private InteractionFinder _interactionFinder;
    private ProjectileSpawner _projectileSpawner;

    protected override void Awake()
    {
        base.Awake();
        _movement.Initialize(this);

        _lastGroundedPosition = transform.position;
        _groundDetector = GetComponent<SolidGroundDetector>();
        _interactionFinder = GetComponent<InteractionFinder>();
        _projectileSpawner = GetComponentInChildren<ProjectileSpawner>();
    }
    protected override void Update()
    {
        if (GamePaused) return;
        if (_groundDetector.OnSolidGround)
            _lastGroundedPosition = transform.position;
        _movement.Update();
        base.Update();
    }
    protected override void FixedUpdate()
    {
        if (GamePaused) return;
        _movement.FixedUpdate();
        base.FixedUpdate();
    }

    protected override void OnDeath()
    {
        Time.timeScale = 0f;
    }

    public void WarpToLastGroundedPosition()
    {
        transform.position = _lastGroundedPosition;
        Movement.Velocity = Vector3.zero;
    }

    public void Strike()
    {
        if (IsAlive)
        {
            if (Struck != null)
                Struck(this, null);
        }
    }
    public void FireArrow()
    {
        _projectileSpawner.SpawnProjectile();
    }
    public void Interact()
    {
        var interaction = _interactionFinder.GetViableInteraction();
        if (interaction != null)
            interaction.Interact(this);
    }
    public void Pause()
    {
        if (IsAlive && Time.timeScale > 0)
        {
            if (RequestedGamePause != null)
                RequestedGamePause(this);
        }
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
