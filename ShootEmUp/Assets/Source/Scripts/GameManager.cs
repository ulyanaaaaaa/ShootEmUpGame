using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Action OnWin;
    [SerializeField] private EnemyPull _enemyPull;
    
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
        Debug.Log("1");
        _wavesCompleted = false;
        _enemyPull.SpawnPoints = _spawnPointsFirstLevel;
        _enemy.EnemiesWaves(_startEnemyCount, _enemiesWaveCountFirstLevel, false);
    }

    private IEnumerator PlayerTransition(Transform position)
    {
        Debug.Log("2");
        _player.NextLevel(position.position, _playerTime);
        yield return new WaitForSeconds(_playerTime);
        Debug.Log("3");
    }

    private void SecondLevel()
    {
        Debug.Log("4");
        _wavesCompleted = false;
        _enemyPull.SpawnPoints = _spawnPointsSecondLevel;
        _enemy.EnemiesWaves(_startEnemyCount * (int)_enemyMultiplier, _enemiesWaveCountSecondLevel, false);
    }

    private void ThirdLevel()
    {
        _wavesCompleted = false;
        _enemyPull.SpawnPoints = _spawnPointsThirdLevel;
        _enemy.EnemiesWaves(_startEnemyCount * (int)_enemyMultiplier, _enemiesWaveCountThirdLevel, true);
    }

    private void WavesCompletedHandler(bool isLastWave)
    {
        _wavesCompleted = true;
        
        if(isLastWave)
            OnWin?.Invoke();
    }
}
