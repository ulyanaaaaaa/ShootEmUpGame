using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyPull _enemyPull;
    private PlayerMovement _player;
    [SerializeField] private float speed = 3f;
    private bool facingRight = true;

    public void Setup(EnemyPull enemyPull)
    {
        _enemyPull = enemyPull;
    }

    private void Start()
    {
        _player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (_player != null)
        {
            Vector3 direction = (_player.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            
            if ((direction.x > 0 && facingRight) || (direction.x < 0 && !facingRight))
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Die()
    {
        gameObject.SetActive(false);
        _enemyPull.SpawnEnemyAtRandomPosition();
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            Debug.Log("Die");
        }
    }
}
