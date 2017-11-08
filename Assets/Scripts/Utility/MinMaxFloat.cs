using UnityEngine;

[System.Serializable]
public struct MinMaxFloat
{
    [SerializeField]
    private float _min;
    [SerializeField]
    private float _max;

    public float Min { get { return _min; } set { _min = value; } }
    public float Max { get { return _max; } set { _max = value; } }

    public MinMaxFloat(float min, float max)
    {
        _min = min;
        _max = max;
    }
    public float GetRandom()
    {
        return Random.Range(Min, Max);
    }
}
