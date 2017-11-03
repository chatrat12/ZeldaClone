using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 10f;

    private void Update()
    {
        transform.position += transform.forward * _moveSpeed * Time.deltaTime;
    }
}
