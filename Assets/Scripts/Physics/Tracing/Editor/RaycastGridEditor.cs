using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RaycastGrid))]
public class RaycastGridEditor : Editor
{
    RaycastGrid _grid;

    private void OnSceneGUI()
    {
        LazyInit();
        DrawSquare(_grid.Width, _grid.Height, _grid.transform.localToWorldMatrix);
        DrawPoints();
    }

    private void DrawSquare(float width, float height, Matrix4x4 matrix)
    {
        using (new Handles.DrawingScope(matrix))
        {
            var halfWidth = width * 0.5f;
            var halfHeight = height * 0.5f;

            // Bottom
            Handles.DrawLine(new Vector2(-halfWidth, -halfHeight), new Vector2(halfWidth, -halfHeight));
            // Right
            Handles.DrawLine(new Vector2(halfWidth, -halfHeight), new Vector2(halfWidth, halfHeight));
            // Top
            Handles.DrawLine(new Vector2(-halfWidth, halfHeight), new Vector2(halfWidth, halfHeight));
            // Left
            Handles.DrawLine(new Vector2(-halfWidth, -halfHeight), new Vector2(-halfWidth, halfHeight));
        }
    }

    private void DrawPoints()
    {
        for (int row = 0; row < _grid.Rows; row++)
        {
            for (int column = 0; column < _grid.Columns; column++)
            {
                Handles.DotHandleCap(0, _grid.GetGridPoint(column, row), Quaternion.identity, 0.01f, EventType.Repaint);
            }
        }
    }


    private void DrawHandles(Matrix4x4 matrix)
    {
        var backupMatrix = Handles.matrix;
        Handles.matrix = matrix;

        //Handles.FreeMoveHandle()

        Handles.matrix = backupMatrix;

    }

    private void LazyInit()
    {
        if (_grid == null)
            _grid = (RaycastGrid)target;
    }
}
