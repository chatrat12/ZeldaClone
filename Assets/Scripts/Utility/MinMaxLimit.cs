
using UnityEngine;

public class MinMaxLimit : System.Attribute
{
    public float Min { get; set; }
    public float Max { get; set; }

    public MinMaxLimit(float minLimit, float maxLimit)
    {
        Min = minLimit;
        Max = maxLimit;
    }
}
