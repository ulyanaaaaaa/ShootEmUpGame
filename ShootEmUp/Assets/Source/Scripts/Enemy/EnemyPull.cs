using UnityEngine;

public class EnemyPull : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject EnemiesParent;
    private Enemy _enemyPrefab; 

    private void Awake()
    {
        _enemyPrefab = Resources.Load<Enemy>("Enemy");
    }
    
    public void SpawnEnemyAtRandomPosition()
    {
        Enemy enemy = Instantiate(_enemyPrefab, EnemiesParent.transform);
        int randomIndex = Random.Range(0, SpawnPoints.Length);
        enemy.transform.position = SpawnPoints[randomIndex].position;
    }
}
