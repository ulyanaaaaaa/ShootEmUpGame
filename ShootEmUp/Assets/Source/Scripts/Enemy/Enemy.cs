using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PlayerMovement _player;
    private SPUM_Prefabs _spumPrefab;
    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    [SerializeField] private float _speed;
    private bool facingRight = true;
    [SerializeField] private LayerMask wallLayer;
    
    private void Start()
    {
        _spumPrefab = GetComponent<SPUM_Prefabs>();
        _player = FindObjectOfType<PlayerMovement>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.gravityScale = 0;
    }

    private void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        if (_player != null)
        {
            Vector2 direction = (_player.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, wallLayer);
            if (hit.collider != null)
            {
                Vector2 leftDirection = Quaternion.Euler(0, 0, 45) * direction;
                Vector2 rightDirection = Quaternion.Euler(0, 0, -45) * direction;
                if (!Physics2D.Raycast(transform.position, leftDirection, 1f, wallLayer))
                {
                    direction = leftDirection;
                }
                else if (!Physics2D.Raycast(transform.position, rightDirection, 1f, wallLayer))
                {
                    direction = rightDirection;
                }
                else
                {
                    direction = Vector2.Reflect(direction, hit.normal);
                }
            }

            _movement = direction;

            if ((direction.x > 0 && facingRight) || (direction.x < 0 && !facingRight))
            {
                Flip();
            }
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition((Vector2)transform.position + (_movement * _speed * Time.fixedDeltaTime));
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
        _spumPrefab.PlayAnimation(2);
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
        StartCoroutine(DieTick(0.5f));
    }

    private IEnumerator DieTick(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            playerMovement.Die();
        }

        if (collision.gameObject.CompareTag("Boundary"))
        {

        }
    }
}
