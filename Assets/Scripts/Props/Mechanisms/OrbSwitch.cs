using UnityEngine;

public class OrbSwitch : Mechanism
{
    [SerializeField]
    private bool _deactivateAfterTime;
    [SerializeField]
    private float _deactivateTime;

    [SerializeField]
    private Renderer[] _glassRenderers;
    [SerializeField]
    private float _emissionLevel = 0f;

    private Material[] _glassMaterials;
    private Color[] _ogEmissionColors;
    private Damagable _damagable;
    private Animator _animator;

    private float _previousEmissionLevel = 0f;
    private float _timeToDeactiavte;
    private bool _activated = false;

    private void Awake()
    {
        _damagable = GetComponent<Damagable>();
        _damagable.TookDamage += _damagable_TookDamage;
        _animator = GetComponent<Animator>();

        _glassMaterials = new Material[_glassRenderers.Length];
        _ogEmissionColors = new Color[_glassRenderers.Length];
        for (int i = 0; i < _glassRenderers.Length; i++)
        {
            _glassMaterials[i] = _glassRenderers[i].material;
            _ogEmissionColors[i] = _glassMaterials[i].GetColor("_EmissionColor");
        }
        _previousEmissionLevel = _emissionLevel;
        SetGlassEmission(_emissionLevel);
    }

    private void _damagable_TookDamage(float baseDamage, GameObject damageCauser, DamageType damageType)
    {
        if (!_activated)
        {
            _animator.SetTrigger("Activate");
            _activated = true;
            if (_deactivateAfterTime)
                _timeToDeactiavte = Time.time + _deactivateTime;
            OnActivate();
        }
    }

    private void Update()
    {
        if (_emissionLevel != _previousEmissionLevel)
        {
            SetGlassEmission(_emissionLevel);
            _previousEmissionLevel = _emissionLevel;
        }
        if(_deactivateAfterTime && _activated)
        {
            if(Time.time >= _timeToDeactiavte)
            {
                _animator.SetTrigger("Deactivate");
                _activated = false;
                OnDeactivate();
            }
        }
    }

    private void SetGlassEmission(float value)
    {
        for (int i = 0; i < _glassMaterials.Length; i++)
        {
            var color = Color.Lerp(Color.black, _ogEmissionColors[i], value);
            _glassMaterials[i].SetColor("_EmissionColor", color);
        }
    }
}
