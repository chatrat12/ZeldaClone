using UnityEngine;

public class VolumeMechanism : Mechanism
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
            OnActivate();
    }
}
