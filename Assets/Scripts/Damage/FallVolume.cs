using UnityEngine;

public class FallVolume : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerController>();
        if(player != null)
        {
            player.WarpToLastGroundedPosition();
            Damage.ApplyGenericDamage(player.gameObject, 2);
        }
    }
}
