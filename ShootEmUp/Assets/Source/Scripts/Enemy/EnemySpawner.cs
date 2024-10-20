using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyPull))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private float _wavesInterval = 3f;
    public EnemyPull EnemyPool;
    private Coroutine _coroutine;
    public event Action<bool> OnWavesCompleted;

    private void Awake()
    {
        EnemyPool = GetComponent<EnemyPull>();
    }

    public void EnemiesWaves(int countEnemy, int countWaves, bool isLastWave)
    {
        _coroutine = StartCoroutine(WaveTick(countEnemy, countWaves, isLastWave));
    }

    private IEnumerator WaveTick(int countEnemy, int countWaves, bool isLastWave)
    {
        for (int i = 0; i < countWaves; i++)
        {
            yield return StartCoroutine(SpawnEnemy(countEnemy));
            yield return new WaitForSeconds(_wavesInterval);
        }

        yield return StartCoroutine(WaitUntilEnemiesCleared(isLastWave));
    }

    private IEnumerator WaitUntilEnemiesCleared(bool isLastWave)
    {
        while (EnemyPool.EnemiesParent.transform.childCount > 0)
            yield return null; 
        
        OnWavesCompleted?.Invoke(isLastWave);
    }

    private IEnumerator SpawnEnemy(int count)
    {
        while (EnemyPool.EnemiesParent.transform.childCount <= count)
        {
            yield return new WaitForSeconds(_spawnInterval);
            EnemyPool.SpawnEnemyAtRandomPosition();
        }
    }
}
