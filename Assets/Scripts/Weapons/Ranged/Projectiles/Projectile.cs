using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float _maxLife = 5f;
    [SerializeField]
    private float _velocity = 25f;
    [SerializeField]
    private LayerMask _layerMask;

    public GameObject Owner { get; set; }

    private Vector3 _previousPosition;

    private void Awake()
    {
        Destroy(this.gameObject, _maxLife);
    }

    private void Update()
    {
        _previousPosition = transform.position;
        transform.position += transform.forward * _velocity * Time.deltaTime;
        Raycast();
    }

    private void Raycast()
    {
        var direction = (transform.position - _previousPosition).normalized;
        var distance = Vector3.Distance(transform.position, _previousPosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(_previousPosition, direction, out hitInfo, distance, _layerMask, QueryTriggerInteraction.Collide))
        {
            Destroy(this.gameObject);
            Damage.ApplyPointDamage(hitInfo.collider.gameObject, 2, Owner, transform.forward, 15, hitInfo);
        }
    }
}
