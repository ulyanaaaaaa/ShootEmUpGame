using UnityEngine;

[RequireComponent(typeof(BoostersPull))]
public class BoostersSpawner : MonoBehaviour
{
    private BoostersPull _objectPool;
    [SerializeField] private float _spawnInterval;
    private Vector2 _spawnAreaMin; 
    private Vector2 _spawnAreaMax;

    private void Awake()
    {
        _objectPool = GetComponent<BoostersPull>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnBonus), _spawnInterval, _spawnInterval);
    }

    public void SetSpawnArea(Vector2 min, Vector2 max)
    {
        _spawnAreaMax = max;
        _spawnAreaMin = min;
    }

    private void SpawnBonus()
    {
        GameObject booster = _objectPool.GetPooledObject();
        if (booster != null)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(_spawnAreaMin.x, _spawnAreaMax.x),
                Random.Range(_spawnAreaMin.y, _spawnAreaMax.y)
            );
            booster.transform.position = randomPosition;
            booster.SetActive(true);
        }
    }
}
