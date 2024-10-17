using UnityEngine;

public class Cartridge : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    private ShootPull _shootPull;
    private Vector2 direction;
    
    public void Setup(ShootPull shootPull)
    {
        _shootPull = shootPull;
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().velocity = direction * _speed;
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
