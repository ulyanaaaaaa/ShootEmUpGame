using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private SPUM_Prefabs _spumPrefab;
    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    private bool _isAttacking = false;
    private bool _facingRight = false;

    private void Awake()
    {
        _spumPrefab = GetComponent<SPUM_Prefabs>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _movement = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _movement.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _movement.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _movement.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _movement.x = 1;
        }

        if (_movement.sqrMagnitude > 0.1f)
        {
            _spumPrefab.PlayAnimation(1);
            if (_movement.x > 0 && !_facingRight)
            {
                Flip();
            }
            else if (_movement.x < 0 && _facingRight)
            {
                Flip();
            }
        }
        else
        {
            _spumPrefab.PlayAnimation(0);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _movement * _moveSpeed * Time.fixedDeltaTime);
    }

    public void Attack()
    {
        if (!_isAttacking)
        {
            _isAttacking = true;
            _spumPrefab.PlayAnimation(4);
            StartCoroutine(EndAttack());
        }
    }

    private void Flip()
    {
        _facingRight = !_facingRight;
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

    public void NextLevel(Vector2 position)
    {
        transform.DOMove(position, 5f);
    }

    private IEnumerator DieTick(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(0.5f);
        _isAttacking = false;
    }
}
