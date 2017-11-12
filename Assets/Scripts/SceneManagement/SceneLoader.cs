using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const string LOADING_SCEEN = "LoadingScreen";
    private const string DUNGEON = "DungeonOne_v2";
    private const string MENU = "Menu";
    private const string HUD = "HUD";
    private const string IN_GAME_MENUS = "InGameMenus";

    public static void LoadDungeon()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(LOADING_SCEEN).completed += delegate
        {
            SceneManager.LoadSceneAsync(DUNGEON, LoadSceneMode.Additive).completed += delegate
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(DUNGEON));
                SceneManager.LoadSceneAsync(HUD, LoadSceneMode.Additive).completed += delegate
                {
                    SceneManager.LoadSceneAsync(IN_GAME_MENUS, LoadSceneMode.Additive).completed += delegate
                    {
                        SceneManager.UnloadSceneAsync(LOADING_SCEEN);
                    };

                };
            };
        };
    }

    public static void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(LOADING_SCEEN).completed += delegate
        {
            SceneManager.LoadSceneAsync(MENU, LoadSceneMode.Additive).completed += delegate
            {
                SceneManager.UnloadSceneAsync(LOADING_SCEEN);
            };
        };
    }

    public static void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void Dungeon()
    {
        LoadDungeon();
    }
    public void Menu()
    {
        LoadMenu();
    }
    public void Quit()
    {
        QuitApplication();
    }
}
