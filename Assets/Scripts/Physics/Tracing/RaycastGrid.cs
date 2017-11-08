using UnityEngine;

public class RaycastGrid : MonoBehaviour
{
    public float Width { get { return _width; } set { _width = value; } }
    public float Height { get { return _height; } set { _height = value; } }
    public int Columns { get { return _columns; } set { _columns = value; } }
    public int Rows { get { return _rows; } set { _rows = value; } }

    [SerializeField]
    private float _width = 1;
    [SerializeField]
    private float _height = 1;
    [Range(1, 10)]
    [SerializeField]
    private int _columns = 3;
    [Range(1, 10)]
    [SerializeField]
    private int _rows = 3;
    [SerializeField]
    private LayerMask _layerMask;

    private float _columnSpacing { get { return _width / (_columns - 1); } }
    private float _rowSpacing { get { return _width / (_rows - 1); } }

    private Matrix4x4 _previousMatrix;

    private void Awake()
    {
        _previousMatrix = transform.localToWorldMatrix;
    }
    private void Update()
    {
        if (transform.localToWorldMatrix != _previousMatrix)
            Raycast();
        _previousMatrix = transform.localToWorldMatrix;
    }
    public Vector3 GetGridPoint(int column, int row)
    {
        return GetGridPoint(column, row, transform.localToWorldMatrix);
    }

    public Vector3 GetGridPoint(int column, int row, Matrix4x4 matrix)
    {
        var offset = new Vector3(_width * 0.5f, _height * 0.5f);
        var pos = new Vector3(column * _columnSpacing, row * _rowSpacing) - offset;
        return matrix.MultiplyPoint(pos);
    }

    private void Raycast()
    {
        RaycastHit hitInfo;
        for (int row = 0; row < _rows; row++)
        {
            for (int column = 0; column < _columns; column++)
            {
                var p1 = GetGridPoint(column, row, _previousMatrix);
                var p2 = GetGridPoint(column, row, transform.localToWorldMatrix);
                var direction = (p2 - p1).normalized;
                var distance = Vector3.Distance(p1, p2);
                if (Physics.Raycast(p1, direction, out hitInfo, distance, _layerMask.value))
                {
                    Debug.Log("We have hit a thing");
                }
            }
        }
    }
}
