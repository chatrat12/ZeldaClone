using UnityEngine;

public class RevealOnActivated : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectToActivate;

    private void Awake()
    {
        GetComponent<Mechanism>().Activated += delegate
        {
            _objectToActivate.SetActive(true);
        };
    }

}
