using UnityEngine;
using UnityEngine.UI;

public class KeysHUD : MonoBehaviour
{
    private Text _text;
    private ItemCollection _playerItemCollection;

    private int _previousNumberOfKeys = 0;

    private void Awake()
    {
        _text = GetComponentInChildren<Text>();
    }
    private void Start()
    {
        _playerItemCollection = GameObject.FindObjectOfType<PlayerController>()
            .GetComponent<ItemCollection>();
        SetKeyNumberText(_playerItemCollection.SilverKeyCount);
    }

    private void LateUpdate()
    {
        if (_playerItemCollection.SilverKeyCount != _previousNumberOfKeys)
            SetKeyNumberText(_playerItemCollection.SilverKeyCount);
    }

    private void SetKeyNumberText(int numberOfKeys)
    {
        _text.text = numberOfKeys.ToString();
        _previousNumberOfKeys = numberOfKeys;
    }

}
