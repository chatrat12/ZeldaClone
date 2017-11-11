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
        SceneManager.LoadScene("DungeonOne_v2");
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);
    }
}
