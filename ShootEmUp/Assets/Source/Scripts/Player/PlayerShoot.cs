using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ShootPull))]
public class PlayerShoot : MonoBehaviour
{
    public ShootPull ShootPull;

    [SerializeField] private float _shootSpeed = 0.1f;
    [SerializeField] private float _shootAccelerant = 5;
    
    private Vector2 _direction = Vector2.zero;
    private PlayerMovement _player;
    
    private bool _isSpawning;
    private bool _isPuckUpShootBooster;

    private void Awake()
    {
        ShootPull = GetComponent<ShootPull>();
        _player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        _direction = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            _direction += Vector2.up;
            _player.Attack();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            _direction += Vector2.down;
            _player.Attack();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            _direction += Vector2.left;
            _player.Attack();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _direction += Vector2.right;
            _player.Attack();
        }

        if (_direction != Vector2.zero && !_isSpawning)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    private IEnumerator SpawnRoutine()
    {
        _isSpawning = true;
        while (_direction != Vector2.zero)
        {
            _direction.Normalize();
            ShootPull.SpawnCartridge(_direction);
            
            if (!_isPuckUpShootBooster)
                yield return new WaitForSeconds(_shootSpeed);
            else
                yield return new WaitForSeconds(_shootSpeed / _shootAccelerant);
        }
        _isSpawning = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ShootBooster shootBooster))
        {
            if (!_isPuckUpShootBooster)
            {
                _isPuckUpShootBooster = true;
                StartCoroutine(BoosterTick(shootBooster.LifeTick));
                Destroy(shootBooster.gameObject);
            }
        }
    }
    
    private IEnumerator BoosterTick(float boosterTime)
    {
        yield return new WaitForSeconds(boosterTime); 
        _isPuckUpShootBooster = false;
    }
}

