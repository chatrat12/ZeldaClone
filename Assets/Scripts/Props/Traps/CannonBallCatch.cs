using UnityEngine;

public class CannonBallCatch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CannonBall>() != null)
            Destroy(other.gameObject);
    }

}
