using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyPull))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 2f;
    public EnemyPull EnemyPool;

    private void Awake()
    {
        EnemyPool = GetComponent<EnemyPull>();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            EnemyPool.SpawnEnemyAtRandomPosition();
        }
    }
}
