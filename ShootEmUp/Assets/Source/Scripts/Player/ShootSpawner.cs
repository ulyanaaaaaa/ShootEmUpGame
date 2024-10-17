using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ShootPull))]
public class ShootSpawner : MonoBehaviour
{
    public ShootPull ShootPull;
    private Vector2 _direction = Vector2.zero;
    private PlayerMovement _player;
    private bool _isSpawning = false;

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
            yield return new WaitForSeconds(0.1f); 
        }
        _isSpawning = false;
    }
}

