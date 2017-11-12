using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{


    private static LoadingScreen _instance;


    public static void Show()
    {
        if (_instance == null)
            SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Additive);
            _instance._canvas.enabled = true;
    }

    public static void Hide()
    {
        _instance._canvas.enabled = false;
    }

    private Canvas _canvas;

    private void Awake()
    {
        _instance = this;
        _canvas = GetComponent<Canvas>();
    }
}
