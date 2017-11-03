using System.Collections.Generic;
using UnityEngine;

public class HealthHUD : MonoBehaviour
{
    [SerializeField]
    private HeartContainerHUD _heartContainerPrefab;

    private List<HeartContainerHUD> _heartContainers = new List<HeartContainerHUD>();
    private Character _player;
    

    private void Start()
    {
        _player = FindObjectOfType<PlayerController>();
        SetNumberOfHeartContainers(_player.MaxHealth / 4);
        UpdateHeartContainers();
        _player.TookDamage += delegate
        {
            UpdateHeartContainers();
        };
    }

    private void UpdateHeartContainers()
    {
        for (int i = 0; i < _heartContainers.Count; i++)
        {
            _heartContainers[i].Value = Mathf.Clamp(_player.Health - i * 4, 0, 4);
        }
    }

    private void SetNumberOfHeartContainers(int number)
    {

        RemoveAllHeartContainers();
        for (int i = 0; i < number; i++)
        {
            _heartContainers.Add(Instantiate(_heartContainerPrefab, transform));
        }
    }

    private void RemoveAllHeartContainers()
    {
        _heartContainers.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
