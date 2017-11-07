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

    private void Awake()
    {
        _character = GetComponent<Character>();
        _renderers = GetComponentsInChildren<Renderer>();
        _originalColors = new Color[_renderers.Length];
        for (int i = 0; i < _renderers.Length; i++)
        {
            _originalColors[i] = _renderers[i].material.color;
        }
        _character.TookDamage += delegate
        {
            TurnDamageColor();
            _lastToggleTime = Time.time;
        };
    }



    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (!_character.IsAlive)
        {
            TurnNormalColor();
            return;
        }

        if (!_character.CanDamage)
        {
            if (Time.time - _lastToggleTime >= _toggleSpeed)
            {
                _lastToggleTime = Time.time;
                ToggleColor();
            }
        }
        else
            TurnNormalColor();
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
