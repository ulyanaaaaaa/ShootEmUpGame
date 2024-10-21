using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Action OnWin;
    
    [SerializeField] private EnemyPull _enemyPull;
    [SerializeField] private BoostersSpawner _boostersSpawner;
    
    [SerializeField] private Transform[] _spawnPointsFirstLevel;
    [SerializeField] private Transform[] _spawnPointsSecondLevel;
    [SerializeField] private Transform[] _spawnPointsThirdLevel;
    
    [SerializeField] private Transform _secondLevelPosition;
    [SerializeField] private Transform _thirdLevelPosition;
    
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private EnemySpawner _enemy;
    
    [SerializeField] private float _playerTime = 5f;
    [SerializeField] private int _startEnemyCount;
    [SerializeField] private float _enemyMultiplier;
    
    [SerializeField] private int _enemiesWaveCountFirstLevel;
    [SerializeField] private int _enemiesWaveCountSecondLevel;
    [SerializeField] private int _enemiesWaveCountThirdLevel;

    [SerializeField] private Vector2 _levelsSize = new Vector2(10f, 10f);

    [SerializeField] private Vector2 _minSpawnBoostersPosition;
    [SerializeField] private Vector2 _maxSpawnBoostersPosition;
    
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
        yield return StartCoroutine(PlayerTransition(_secondLevelPosition));
        SecondLevel();
        yield return new WaitUntil(() => _wavesCompleted); 
        yield return StartCoroutine(PlayerTransition(_thirdLevelPosition));
        ThirdLevel();
    }

    private void FirstLevel()
    {
        _wavesCompleted = false;
        _enemyPull.SpawnPoints = _spawnPointsFirstLevel;
        _boostersSpawner.SetSpawnArea(_minSpawnBoostersPosition, _maxSpawnBoostersPosition);
        _enemy.EnemiesWaves(_startEnemyCount, _enemiesWaveCountFirstLevel, false);
    }

    private IEnumerator PlayerTransition(Transform position)
    {
        _player.NextLevel(position.position, _playerTime);
        yield return new WaitForSeconds(_playerTime);
    }

    private void SecondLevel()
    {
        _wavesCompleted = false;
        _enemyPull.SpawnPoints = _spawnPointsSecondLevel;
        _boostersSpawner.SetSpawnArea(_minSpawnBoostersPosition + _levelsSize,
            _maxSpawnBoostersPosition + _levelsSize);
        _enemy.EnemiesWaves(_startEnemyCount * (int)_enemyMultiplier, _enemiesWaveCountSecondLevel, false);
    }

    private void ThirdLevel()
    {
        _wavesCompleted = false;
        _enemyPull.SpawnPoints = _spawnPointsThirdLevel;
        _boostersSpawner.SetSpawnArea(_minSpawnBoostersPosition + 2 * _levelsSize,
            _maxSpawnBoostersPosition + 2 * _levelsSize);
        _enemy.EnemiesWaves(_startEnemyCount * (int)_enemyMultiplier, _enemiesWaveCountThirdLevel, true);
    }

    private void WavesCompletedHandler(bool isLastWave)
    {
        _wavesCompleted = true;
        
        if(isLastWave)
            OnWin?.Invoke();
    }
}
