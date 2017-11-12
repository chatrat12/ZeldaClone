using UnityEngine;

public class MainMenu : MonoBehaviour
{

	public void Play()
    {
        SceneLoader.LoadDungeon();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }
}
