using UnityEngine;

public class WonderEnemy : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 10f;

    private Vector3 _targetDirection;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _targetDirection = GetRandomXZDirection();
    }

    private Vector3 GetRandomXZDirection()
    {
        var v = Random.insideUnitCircle;
        return new Vector3(v.x, 0, v.y).normalized;
    }

    private void Update()
    {
        _rigidbody.velocity = _targetDirection * _moveSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var normal = collision.contacts[0].normal;
        normal.y = 0;
        normal.Normalize();
        ReflectVelocity(normal);
    }
    

    private void ReflectVelocity(Vector3 normal)
    {
        //var output = _targetDirection.ToString() + " " + normal.ToString();
        _targetDirection = Vector3.Reflect(_targetDirection, normal);
        //transform.position += _targetDirection * Time.fixedDeltaTime;
        //output += " " + _targetDirection.ToString();
        //Debug.Log(output);
    }

}
