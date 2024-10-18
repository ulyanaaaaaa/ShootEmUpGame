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
    public event Action OnWavesCompleted;

    private void Awake()
    {
        EnemyPool = GetComponent<EnemyPull>();
    }

    public void EnemiesWaves(int countEnemy, int countWaves)
    {
        _coroutine = StartCoroutine(WaveTick(countEnemy, countWaves));
    }

    private IEnumerator WaveTick(int countEnemy, int countWaves)
    {
        for (int i = 0; i < countWaves; i++)
        {
            yield return StartCoroutine(SpawnEnemy(countEnemy));
            yield return new WaitForSeconds(_wavesInterval);
        }
      
        OnWavesCompleted?.Invoke(); 
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
