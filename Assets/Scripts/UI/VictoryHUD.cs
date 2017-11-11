using UnityEngine;

public class VictoryHUD : MonoBehaviour
{
    private CanvasGroup _group;
    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _group = GetComponent<CanvasGroup>();
        _canvas.enabled = false;
    }

    public void Show()
    {
        _canvas.enabled = true;
        Time.timeScale = 0;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
