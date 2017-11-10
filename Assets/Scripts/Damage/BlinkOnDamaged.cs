using UnityEngine;

public class BlinkOnDamaged : MonoBehaviour
{
    [SerializeField]
    private float _toggleSpeed = 0.2f;
    private float _lastToggleTime = 0f;

    private Character _character;
    private Renderer[] _renderers;
    private Color[] _originalColors;

    private bool _isDamagedColor = false;
    private bool _blinking = false;

    private void Awake()
    {
        _character = GetComponent<Character>();
        _renderers = GetComponentsInChildren<Renderer>();
        _originalColors = new Color[_renderers.Length];
        UpdateOGColors();
        _character.TookDamage += delegate
        {
            UpdateOGColors();
            TurnDamageColor();
            _lastToggleTime = Time.time;
            _blinking = true;
        };
    }

    private void UpdateOGColors()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _originalColors[i] = _renderers[i].material.color;
        }
    }

    
    private void LateUpdate()
    {
        if (!_character.IsAlive)
        {
            TurnNormalColor();
            return;
        }

        if (_blinking)
        {
            if (Time.time - _lastToggleTime >= _toggleSpeed)
            {
                _lastToggleTime = Time.time;
                ToggleColor();
            }
            if(_character.CanDamage)
            {
                _blinking = false;
                TurnNormalColor();
            }
        }
    }

    private void ToggleColor()
    {
        if (_isDamagedColor)
            TurnNormalColor();
        else
            TurnDamageColor();
    }

    private void TurnNormalColor()
    {
        _isDamagedColor = false;
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].material.color = _originalColors[i];
        }
    }
    private void TurnDamageColor()
    {
        _isDamagedColor = true;
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].material.color = (_originalColors[i] + Color.red) * 0.5f;
        }
    }
}
