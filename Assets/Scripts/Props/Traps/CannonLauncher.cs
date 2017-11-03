using UnityEngine;

public class CannonLauncher : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private float _fireRate = 1f;
    [SerializeField]
    private int _fireLargeEveryNth = 4;

    [Header("Setup")]
    [SerializeField]
    private Transform _smallSpawnATransfrom;
    [SerializeField]
    private Transform _smallSpawnBTransform;
    [SerializeField]
    private Transform _largeSpawnTransform;

    [SerializeField]
    private GameObject _cannonBallSmallPrefab;
    [SerializeField]
    private GameObject _cannonBallLargePrefab;

    private float _lastFireTime = 0f;
    private int _smallCount = 0;

    private void Update()
    {
        if(Time.time - _lastFireTime >= _fireRate)
        {
            FireCannonBall();
        }
    }

    private void FireCannonBall()
    {
        _lastFireTime = Time.time;
        if (_smallCount >= _fireLargeEveryNth)
            FireLargeCannonBall();
        else
            FireSmallCannonBall();
    }

    private void FireSmallCannonBall()
    {
        var spawnTransform = Random.Range(0, 2) == 0 ? _smallSpawnATransfrom : _smallSpawnBTransform;
        SpawnCannonBall(_cannonBallSmallPrefab, spawnTransform);
        _smallCount++;
    }
    private void FireLargeCannonBall()
    {
        SpawnCannonBall(_cannonBallLargePrefab, _largeSpawnTransform);
        _smallCount = 0;
    }
    private void SpawnCannonBall(GameObject prefab, Transform transform)
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }

}
