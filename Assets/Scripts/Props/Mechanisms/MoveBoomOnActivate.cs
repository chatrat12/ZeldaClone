using System.Collections;
using UnityEngine;

// Super hacky stuff
public class MoveBoomOnActivate : MonoBehaviour
{
    [SerializeField]
    private Transform _moveTarget;
    [SerializeField]
    private float _secondsMoved = 1f;

    private Transform _previousBoomTarget;
    private CameraBoom _boom;

    private void Awake()
    {
        var mechanism = GetComponent<Mechanism>();
        mechanism.Activated += Mechanism_Activated;
    }

    private void Mechanism_Activated(Mechanism mechanism)
    {
        if(_boom == null)
            _boom = FindObjectOfType<CameraBoom>(); // <-- Gross!
        _previousBoomTarget = _boom.Target;
        _boom.Target = _moveTarget;
        StartCoroutine(MoveBack()); // <-- Also Gross!
        Time.timeScale = 0; 
    }

    private IEnumerator MoveBack()
    {
        yield return new WaitForSecondsRealtime(_secondsMoved);
        _boom.Target = _previousBoomTarget;
        Time.timeScale = 1;
    }
}
