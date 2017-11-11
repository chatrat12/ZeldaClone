using UnityEngine;

public class VictoryHUDActivator : MonoBehaviour
{
    private VictoryHUD _hud;

    private void Awake()
    {
        _hud = FindObjectOfType<VictoryHUD>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
            _hud.Show();
    }
}
