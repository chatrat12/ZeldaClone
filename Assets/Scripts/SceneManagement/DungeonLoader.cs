using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonLoader : MonoBehaviour
{
    private void Awake()
    {
        LoadDungeon();
    }

    public static void LoadDungeon()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync("DungeonOne_v2");
        SceneManager.LoadSceneAsync("HUD", LoadSceneMode.Additive);
    }
}
