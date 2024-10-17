using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private PlayerMovement _player;
    private SPUM_Prefabs _spumPrefab;
    [SerializeField] private float _speed = 3f;
    private bool facingRight = true;

    private void Start()
    {
        _spumPrefab = GetComponent<SPUM_Prefabs>();
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
            transform.position += direction * _speed * Time.deltaTime;
            _spumPrefab.PlayAnimation(1);
            
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
    }
}
