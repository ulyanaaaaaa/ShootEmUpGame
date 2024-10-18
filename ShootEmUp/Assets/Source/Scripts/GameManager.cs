using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyPull _enemyPull;
    [SerializeField] private Transform[] _spawnPointsFirstLevel;
    [SerializeField] private Transform[] _spawnPointsSecondLevel;
    [SerializeField] private Transform _secondLevelPosition;
    [SerializeField] private Vector3 _cameraSecondPosition;
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private EnemySpawner _enemy;
    [SerializeField] private float _playerTime = 5f;
    [SerializeField] private int _startEnemyCount;
    [SerializeField] private float _enemyMultiplier;
    [SerializeField] private int _enemiesWaveCountFirstLevel;
    [SerializeField] private int _enemiesWaveCountSecondLevel;
    private Coroutine _coroutine;
    private Coroutine _gameCycle;
    private bool _wavesCompleted;

    private void Start()
    {
        _enemy.OnWavesCompleted += WavesCompletedHandler;
        _gameCycle = StartCoroutine(GameCycle());
    }

    private IEnumerator GameCycle()
    {
        FirstLevel();
        yield return new WaitUntil(() => _wavesCompleted); 
        yield return StartCoroutine(PlayerTransition());
        SecondLevel();
    }

    private void FirstLevel()
    {
        _wavesCompleted = false;
        _enemyPull.SpawnPoints = _spawnPointsFirstLevel;
        _enemy.EnemiesWaves(_startEnemyCount, _enemiesWaveCountFirstLevel);
    }

    private IEnumerator PlayerTransition()
    {
        _player.NextLevel(_secondLevelPosition.position, _cameraSecondPosition, _playerTime);
        yield return new WaitForSeconds(_playerTime);
    }

    private void SecondLevel()
    {
        _enemyPull.SpawnPoints = _spawnPointsSecondLevel;
        _enemy.EnemiesWaves(_startEnemyCount * (int)_enemyMultiplier, _enemiesWaveCountSecondLevel);
    }

    private void WavesCompletedHandler()
    {
        _wavesCompleted = true;
    }
}
