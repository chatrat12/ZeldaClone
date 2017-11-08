#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

public static class HandlesExtensions
{
    private static Quaternion _x90 = Quaternion.Euler(90, 0, 0);
    private static Quaternion _x180 = Quaternion.Euler(180, 0, 0);
    private static Quaternion _y90 = Quaternion.Euler(0, 90, 0);
    private static Quaternion _y180 = Quaternion.Euler(0, 180, 0);


    public static void DrawCapsule(Vector3 position, Quaternion rotation, float height, float radius)
    {
        var up = rotation * Vector3.up;
        var left = rotation * Vector3.left;
        var forward = rotation * Vector3.forward;

        if (height < radius * 2)
            height = radius * 2;

        var halfHeightMinusRadius = height * 0.5f - radius;

        var topBase = position + up * halfHeightMinusRadius;
        var bottomBase = position + -up * halfHeightMinusRadius;

        DrawHemisphere(topBase, rotation, radius);
        DrawHemisphere(bottomBase, rotation * _x180, radius);

        Handles.DrawLine(topBase + left * radius,
                         bottomBase + left * radius);
        Handles.DrawLine(topBase + -left * radius,
                         bottomBase + -left * radius);
        Handles.DrawLine(topBase + forward * radius,
                         bottomBase + forward * radius);
        Handles.DrawLine(topBase + -forward * radius,
                         bottomBase + -forward * radius);
    }

    private static void DrawHemisphere(Vector3 position, Quaternion rotation, float radius)
    {
        var left = rotation * Vector3.left;
        var forward = rotation * Vector3.forward;

        Handles.CircleHandleCap(0, position, rotation * _x90, radius, EventType.Repaint);
        Handles.DrawWireArc(position, left, forward, 180, radius);
        Handles.DrawWireArc(position, forward, -left, 180, radius);
    }

    public static void DrawRoundedCube(Vector3 position, Quaternion rotation, Vector3 size, float radius)
    {
        radius = Mathf.Min(size.x * 0.5f, size.y * 0.5f, size.z * 0.5f, radius);

        var up = rotation * Vector3.up;
        var right = rotation * Vector3.right;
        var forward = rotation * Vector3.forward;

        Vector3 halfSize = size * 0.5f;
        Vector3 baseZPos = position + forward * (halfSize.z- radius);

        DrawFourCorners(baseZPos, rotation, size, radius);
        DrawFourCorners(-baseZPos, rotation * _y180, size, radius);

        Vector3 innerSize = size - Vector3.one * radius * 2;
        DrawRectangle(position + forward * halfSize.z, rotation, innerSize.x, innerSize.y);
        DrawRectangle(position + forward * -halfSize.z, rotation, innerSize.x, innerSize.y);
        DrawRectangle(position + right * halfSize.x, rotation * _y90, innerSize.z, innerSize.y);
        DrawRectangle(position + -right * halfSize.x, rotation * _y90, innerSize.z, innerSize.y);
        DrawRectangle(position + up * halfSize.y, rotation * _x90, innerSize.x, innerSize.z);
        DrawRectangle(position + -up * halfSize.y, rotation * _x90, innerSize.x, innerSize.z);
    }

    private static void DrawFourCorners(Vector3 position, Quaternion rotation, Vector3 size, float radius)
    {
        var up = rotation * Vector3.up;
        var left = rotation * Vector3.left;

        Vector3 xOffset = left * (size.x * 0.5f - radius);
        Vector3 yOffset = up * (size.y * 0.5f - radius);

        DrawCorner(position - xOffset + yOffset, rotation, radius);
        DrawCorner(position - xOffset - yOffset, rotation * Quaternion.Euler(90, 0, 0), radius);
        DrawCorner(position + xOffset + yOffset, rotation * Quaternion.Euler(0, -90, 0), radius);
        DrawCorner(position + xOffset - yOffset, rotation * Quaternion.Euler(90, -90, 0), radius);
    }

    private static void DrawCorner(Vector3 position, Quaternion rotation, float radius)
    {
        var up = rotation * Vector3.up;
        var left = rotation * Vector3.left;
        var forward = rotation * Vector3.forward;

        Handles.DrawWireArc(position, left, forward, 90, radius);
        Handles.DrawWireArc(position, forward, -left, 90, radius);
        Handles.DrawWireArc(position, up, forward, 90, radius);
    }

    private static void DrawRectangle(Vector3 position, Quaternion rotation, float width, float height)
    {
        var matrix = Matrix4x4.TRS(position, rotation, new Vector3(width, height, 0));

        var p1 = matrix.MultiplyPoint(new Vector3(-0.5f, -0.5f)); // Bottom Left 
        var p2 = matrix.MultiplyPoint(new Vector3( 0.5f, -0.5f)); // Bottom Right
        var p3 = matrix.MultiplyPoint(new Vector3( 0.5f,  0.5f)); // Top Right
        var p4 = matrix.MultiplyPoint(new Vector3(-0.5f,  0.5f)); // Top Left

        Handles.DrawLine(p1, p2);
        Handles.DrawLine(p2, p3);
        Handles.DrawLine(p3, p4);
        Handles.DrawLine(p4, p1);
    }

}

#endif