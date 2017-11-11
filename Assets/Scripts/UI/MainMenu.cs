using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Canvas _loadingCanvas;

	public void Play()
    {
        _loadingCanvas.enabled = true;
        Debug.Log("show loading screen");
        DungeonLoader.LoadDungeon();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
