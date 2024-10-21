using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Cartridge : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    private Vector2 direction;
    private Rigidbody2D _rigidbody;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.velocity = direction * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.Die();
            Die();
        }
        if (collision.gameObject.CompareTag("Boundary"))
        {
            Die();
        }
    }

    private void Die()
    {
       Destroy(gameObject);
    }
}
