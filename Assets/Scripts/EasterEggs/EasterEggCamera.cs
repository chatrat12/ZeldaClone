using UnityEngine;

[DefaultExecutionOrder(10)]
public class EasterEggCamera : MonoBehaviour
{
    [SerializeField]
    private Vector3 _boomOffset;
    [SerializeField]
    private Vector3 _boomRotation;

    private GameObject _boomTarget;
    private Vector3 _ogBoomOffset;
    private Quaternion _ogBoomRotation;
    private Transform _ogBoomTarget;

    private PlayerController _player;
    private CameraBoom _boom;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
        _boomTarget = new GameObject("Boom Target");
        _boomTarget.transform.SetParent(this.transform);
        _boom = FindObjectOfType<CameraBoom>(); // <-- Gross!
    }

    private void Update()
    {
        var pos = transform.position;
        pos.x = _player.transform.position.x;
        _boomTarget.transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            _ogBoomOffset = _boom.Offset;
            _ogBoomRotation = _boom.transform.rotation;
            _ogBoomTarget = _boom.Target;
            _boom.Target = _boomTarget.transform;
            _boom.Offset = _boomOffset;
            _boom.transform.rotation = Quaternion.Euler(_boomRotation);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            _boom.Target = _ogBoomTarget;
            _boom.Offset = _ogBoomOffset;
            _boom.transform.rotation = _ogBoomRotation;
        }
    }

}
