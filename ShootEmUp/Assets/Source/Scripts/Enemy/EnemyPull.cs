using System.Collections.Generic;
using UnityEngine;

public class EnemyPull : MonoBehaviour
{
    [SerializeField] private int _poolSize = 20; 
    [SerializeField] private Transform[] _spawnPoints; 
    private Enemy _enemyPrefab; 
    private List<GameObject> _pool;

    private void Awake()
    {
        _enemyPrefab = Resources.Load<Enemy>("Enemy");
    }

    private void Start()
    {
        _pool = new List<GameObject>();
        for (int i = 0; i < _poolSize; i++)
        {
            Enemy enemy = Instantiate(_enemyPrefab);
            enemy.Setup(GetComponent<EnemySpawner>().EnemyPool);
            enemy.gameObject.SetActive(false);
            _pool.Add(enemy.gameObject);
        }
    }

    private GameObject GetPooledObject()
    {
        foreach (GameObject obj in _pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        
        Enemy enemy = Instantiate(_enemyPrefab);
        enemy.gameObject.SetActive(false);
        _pool.Add(enemy.gameObject);
        return enemy.gameObject;
    }

    public void SpawnEnemyAtRandomPosition()
    {
        GameObject enemy = GetPooledObject();
        if (enemy != null)
        {
            int randomIndex = Random.Range(0, _spawnPoints.Length);
            enemy.transform.position = _spawnPoints[randomIndex].position;
            enemy.SetActive(true);
        }
    }
}
