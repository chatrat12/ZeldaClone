using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private Text _resoltionText;

    private int _resoltionIndex = 0;

    private void Awake()
    {
        _resoltionText.text = Screen.resolutions[Screen.resolutions.Length - 1].ToString();
        
        //_resolutions = Screen.resolutions;
        
    }

    public void NextResolution()
    {
        _resoltionIndex++;
        if (_resoltionIndex == Screen.resolutions.Length)
            _resoltionIndex = 0;
        _resoltionText.text = Screen.resolutions[_resoltionIndex].ToString();
    }
    public void PreviousResolution()
    {
        _resoltionIndex--;
        if (_resoltionIndex < 0)
            _resoltionIndex = Screen.resolutions.Length - 1;
        _resoltionText.text = Screen.resolutions[_resoltionIndex].ToString();
    }
    public void TogglePostProcessing(Toggle toggle)
    {
        var pp = Camera.main.GetComponent<UnityEngine.PostProcessing.PostProcessingBehaviour>();
        pp.enabled = toggle.isOn;
    }

    private int GetScreenResolution()
    {
        var cr = Screen.currentResolution;
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            var r = Screen.resolutions[i];
            if(ResolutionEqual(cr, r))
                return i;
        }
        return -1;
    }

    private bool ResolutionEqual(Resolution a, Resolution b)
    {
        return a.width == b.width && a.height == b.height && a.refreshRate == b.refreshRate;
    }

    public void Test()
    {
        Debug.Log("Hi");
        var r = Screen.resolutions[Screen.resolutions.Length - 1];
        Screen.SetResolution(1920, 1080, false);
    }
}
