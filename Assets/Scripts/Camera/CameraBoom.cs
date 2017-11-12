using UnityEngine;

// Concept here is kind of meh
public class CameraBoom : MonoBehaviour
{
    public Vector3 Offset { get; set; }
    public Transform Target { get; set; }

    private void Update()
    {
        transform.position = Target.transform.position + Offset;
    }
}
