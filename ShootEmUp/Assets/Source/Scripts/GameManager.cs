using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyPull _enemyPull;
    [SerializeField] private Transform[] _spawnPointsFirstLevel;
    [SerializeField] private Transform[] _spawnPointsSecondLevel;
    [SerializeField] private float _directionFirst;
    [SerializeField] private float _directionSecond;
    [SerializeField] private Transform _secondLevelPosition;
    private PlayerMovement _player;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }

    private void Start()
    {
        StartCoroutine(LevelTick(_directionFirst));
    }

    private IEnumerator LevelTick(float delay)
    {
        _enemyPull.SpawnPoints = _spawnPointsFirstLevel;
        yield return new WaitForSeconds(delay);
        _player.NextLevel(_secondLevelPosition.position);
        yield return new WaitForSeconds(5f);
        _enemyPull.SpawnPoints = _spawnPointsSecondLevel;
    }
}
