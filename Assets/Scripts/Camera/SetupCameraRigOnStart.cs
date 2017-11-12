using UnityEngine;

public class SetupCameraRigOnStart : MonoBehaviour
{
    private void Start()
    {
        var player = GetComponentInParent<PlayerController>();

        // Create boom
        var boomGO = new GameObject("Camera Boom");
        boomGO.transform.position = player.transform.position;
        boomGO.transform.rotation = transform.rotation;
        var boom = boomGO.AddComponent<CameraBoom>();
        boom.Offset = transform.localPosition;

        var cameraOffset = player.GetComponentInChildren<AdaptiveCameraOffset>();
        boom.Target = cameraOffset.transform;

        // Unchild camera
        transform.SetParent(null);

        // Set camera target as boom
        var playerCam = GetComponent<PlayerCamera>();
        playerCam.Target = boom.transform;
    }
}
