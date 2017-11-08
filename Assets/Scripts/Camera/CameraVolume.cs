using UnityEngine;

public class CameraVolume : MonoBehaviour
{
    private BoxCollider _boxCollider;

    private static Vector3 _viewportBottom = new Vector3(0.5f, 0);
    private static Vector3 _viewportTop = new Vector3(0.5f, 1f);
    private static Vector3 _viewportLeft = new Vector3(0, 0.5f);
    private static Vector3 _viewportRight = new Vector3(1, 0.5f);
    private static Plane _plane = new Plane();

    private void Awake()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    public Vector3 ConstrainTargetPosition(Camera camera, Vector3 targetPosition)
    {
        _plane.SetNormalAndPosition(Vector3.down, _boxCollider.bounds.max);

        var rightRay = camera.ViewportPointToRay(_viewportRight);
        var topRay = camera.ViewportPointToRay(_viewportTop);
        var bottomRay = camera.ViewportPointToRay(_viewportBottom);
        float distance;

        if (_plane.Raycast(rightRay, out distance))
        {
            var p = rightRay.GetPoint(distance);
            var xDif = p.x - camera.transform.position.x;

            var maxX = _boxCollider.bounds.max.x - xDif;
            var minX = _boxCollider.bounds.min.x + xDif;
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        }
        if (_plane.Raycast(topRay, out distance))
        {
            var p = topRay.GetPoint(distance);
            var zDif = p.z - camera.transform.position.z;

            var maxZ = _boxCollider.bounds.max.z - zDif;
            targetPosition.z = Mathf.Min(targetPosition.z, maxZ);

        }
        if (_plane.Raycast(bottomRay, out distance))
        {
            var p = bottomRay.GetPoint(distance);
            var zDif = camera.transform.position.z - p.z;

            var minZ = _boxCollider.bounds.min.z + zDif;
            targetPosition.z = Mathf.Max(targetPosition.z, minZ);

        }
        return targetPosition;
    }
}
