using UnityEngine;

public class SwampKid : Damagable
{
    [SerializeField]
    private float _offsetToStartPersuit = 0.5f;
    [SerializeField]
    private float _offsetToStopPersuit = 0.1f;
    [SerializeField]
    private float _boundsPadding = 1f;

    private Animator _animator;
    private PlayerController _player;

    private SwampKidState _state = SwampKidState.Idle;
    private float _min;
    private float _max;

    private float _offsetFromPlayer
    {
        get { return _player.transform.position.x - transform.position.x; }
    }
    private bool _canWalk
    {
        get
        {
            var offset = _offsetFromPlayer;
            if (offset < 0)
                return transform.position.x > _min;
            else
                return transform.position.x < _max;
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerController>();

        var bounds = transform.parent.GetComponent<BoxCollider>().bounds;
        var renderer = GetComponent<SpriteRenderer>();
        _min = bounds.min.x + _boundsPadding;
        _max = bounds.max.x - _boundsPadding;

        TookDamage += delegate
        {
            _animator.SetTrigger("Damage");
        };
    }

    private void Update()
    {
        if(_state == SwampKidState.Idle)
        {
            if(Mathf.Abs(_offsetFromPlayer) > _offsetToStartPersuit)
            {
                _state = SwampKidState.Chasing;
                _animator.SetBool("Walking", true);
            }
        }
        else if(_state == SwampKidState.Chasing)
        {
            if(Mathf.Abs(_offsetFromPlayer) <= _offsetToStopPersuit)
            {
                _state = SwampKidState.Idle;
                _animator.SetBool("Walking", false);
                _animator.SetFloat("Direction", 0);
            }
            else
            {
                _animator.SetFloat("Direction", _offsetFromPlayer > 0 ? 1 : -1);
                _animator.SetBool("Walking", _canWalk);
            }
        }
    }

    private void MakePlayerBlack()
    {
        var renderers = _player.GetComponentsInChildren<Renderer>();
        foreach(var r in renderers)
        {
            r.material.color = r.material.color * 0.15f;
        }
    }

    enum SwampKidState
    {
        Idle,
        Chasing
    }
}