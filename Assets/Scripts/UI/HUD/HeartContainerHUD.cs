using UnityEngine;
using UnityEngine.UI;

public class HeartContainerHUD : MonoBehaviour
{
    public int Value
    {
        get { return _value; }
        set
        {
            _value = value;
            UpdateSprite();
        }
    }


    [SerializeField]
    private Sprite[] _sprites;

    private int _value = 0;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        try
        {
            _image.sprite = _sprites[_value];
        }
        catch
        {
            Debug.Log("Damn");
        }
    }
}