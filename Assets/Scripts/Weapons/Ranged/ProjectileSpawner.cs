using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField]
    private Projectile _prefab;
    [SerializeField]
    private float _spawnRate = 1;

    private float _lastSpawnTime;
    private Character _owner;

    private void Awake()
    {
        _owner = GetComponentInParent<Character>();
    }
    public void SpawnProjectile()
    {
        var projectile = Instantiate(_prefab, transform.position, transform.rotation);
        projectile.Owner = _owner.gameObject;
    }
}
