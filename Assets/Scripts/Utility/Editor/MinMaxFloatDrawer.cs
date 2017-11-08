using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomPropertyDrawer(typeof(MinMaxFloat))]
public class MinMaxFloatDrawer : PropertyDrawer
{
    private const float _prefixWidth = 30;

    private static GUIContent _minLabel = new GUIContent("Min");
    private static GUIContent _maxLabel = new GUIContent("Max");



    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        MinMaxLimit limits = null;
        var attributes = fieldInfo.GetCustomAttributes(typeof(MinMaxLimit), true);
        foreach (var attribute in attributes)
        {
            if (attribute is MinMaxLimit)
            {
                limits = attribute as MinMaxLimit;
                break;
            }
        }
        

        var minProperty = property.FindPropertyRelative("_min");
        var maxProperty = property.FindPropertyRelative("_max");

        float minLimit = 0;
        float maxLimit = 1;

        if(limits != null)
        {
            minLimit = limits.Min;
            maxLimit = limits.Max;
        }

        float min = minProperty.floatValue;
        float max = maxProperty.floatValue;

        position.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.MinMaxSlider(position, label, ref min, ref max, minLimit, maxLimit);

        float floatFieldWidth = (position.width - EditorGUIUtility.labelWidth) * 0.5f;

        // Min float field
        position.y += position.height;
        position.x = EditorGUIUtility.labelWidth;
        position.width = _prefixWidth;
        EditorGUI.PrefixLabel(position, _minLabel);
        position.x += _prefixWidth;
        position.width = floatFieldWidth - _prefixWidth;
        min = EditorGUI.FloatField(position, min);

        // Max float field
        position.x += position.width;
        position.width = _prefixWidth;
        EditorGUI.PrefixLabel(position, _maxLabel);
        position.x += _prefixWidth;
        position.width = floatFieldWidth - _prefixWidth;
        max = EditorGUI.FloatField(position, max);

        minProperty.floatValue = min;
        maxProperty.floatValue = max;


        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight * 2;
    }
}
