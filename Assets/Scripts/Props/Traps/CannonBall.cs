using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 10f;

    private void Update()
    {
        transform.position += transform.forward * _moveSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            if (player.CanDamage)
            {
                // TODO: implement knockback system to handle this.
                Debug.Log("knock back");
                var direction = (player.transform.position - transform.position).normalized;
                direction = (direction + transform.forward * 3).normalized;
                player.Movement.Knockback(direction * 30);
            }
        }
        Damage.ApplyGenericDamage(other.gameObject, 4);
    }
}
