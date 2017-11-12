using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Button _firstToSelect;

    private Canvas _canvas;
    private bool _paused = false;

    private PlayerController _player;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _player = FindObjectOfType<PlayerController>();
        if(_player != null)
            _player.RequestedGamePause += Player_RequestedGamePause;
    }
    private void Update()
    {
        if(_paused)
        {
            if (Input.GetButtonDown("Pause") || Input.GetButtonDown("Cancel"))
                UnPause();
        }
    }

    private void Player_RequestedGamePause(PlayerController sender)
    {
        if (_paused)
            UnPause();
        else
            Pause();
    }

    public void Pause()
    {
        _paused = true;
        _canvas.enabled = true;
        _firstToSelect.Select();
        Time.timeScale = 0;
        _player.GamePaused = true;
        Input.ResetInputAxes();
    }

    public void UnPause()
    {
        _player.GamePaused = false;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
        _paused = false;
        _canvas.enabled = false;
        Time.timeScale = 1;
    }

}
