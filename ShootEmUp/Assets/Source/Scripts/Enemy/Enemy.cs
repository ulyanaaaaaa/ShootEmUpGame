using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyPull _enemyPull;

    public void Setup(EnemyPull enemyPull)
    {
        _enemyPull = enemyPull;
    }

    public void Die()
    {
        gameObject.SetActive(false);
        _enemyPull.SpawnEnemyAtRandomPosition();
    }
}
