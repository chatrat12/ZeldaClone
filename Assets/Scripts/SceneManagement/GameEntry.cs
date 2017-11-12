using UnityEngine;

public class GameEntry : MonoBehaviour
{
    [SerializeField]
    private SceneType _sceneToLoad;

    private void Awake()
    {
        switch (_sceneToLoad)
        {
            case SceneType.Menu:
                SceneLoader.LoadMenu();
                break;
            case SceneType.Dungeon:
                SceneLoader.LoadDungeon();
                break;
        }
    }

    enum SceneType
    {
        Menu,
        Dungeon
    }

}
