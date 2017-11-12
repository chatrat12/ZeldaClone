using UnityEngine;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField]
    private Button _firstSelectedButton;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        FindObjectOfType<PlayerAnimationEvents>().EventTriggered += GameOverMenu_EventTriggered;
    }

    private void GameOverMenu_EventTriggered(PlayerAnimationEvents.EventType type)
    {
        if (type == PlayerAnimationEvents.EventType.FinishedDeath)
        {
            _animator.SetTrigger("Show");
        }
    }

    public void MenuVisible()
    {
        _firstSelectedButton.Select();
    }
}
