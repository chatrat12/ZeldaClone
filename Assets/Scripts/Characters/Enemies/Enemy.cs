using UnityEngine;

public class Enemy : Character
{
    protected Vector3 _spawnPosition;

    protected override void Awake()
    {
        base.Awake();
        _spawnPosition = transform.position;
    }

    public void WarpToSpawn()
    {
        transform.position = _spawnPosition;
    }	
}
