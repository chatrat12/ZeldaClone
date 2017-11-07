using UnityEngine;

public class BlinkOnDamaged : MonoBehaviour
{
    private Character _character;
    private Color _ogColor;
    private Material _material;
    //private Renderer[] _renderers;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
        _ogColor = _material.color;
        _character = GetComponentInParent<Character>();
    }
    private void Update()
    {
        _material.color = _character.CanDamage ? _ogColor : Color.red;
    }

}
