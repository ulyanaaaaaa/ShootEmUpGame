using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Action OnDie;
    
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _invisibleTime = 2f;
    
    private Camera _mainCamera;
    private SPUM_Prefabs _spumPrefab;
    private Rigidbody2D _rigidbody;
    private Vector2 _movement;
    
    private bool _isAttacking;
    private bool _facingRight;
    
    private bool _isPuckUpHealthBooster;
    private bool _isPuckUpLowSpeedDebuf;

    private void Awake()
    {
        _spumPrefab = GetComponent<SPUM_Prefabs>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _mainCamera = Camera.main;
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
        if (!_isPuckUpLowSpeedDebuf)
            _rigidbody.MovePosition(_rigidbody.position + _movement * _moveSpeed * Time.fixedDeltaTime);
        else
            _rigidbody.MovePosition(_rigidbody.position + _movement * _moveSpeed / 2 * Time.fixedDeltaTime);
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
        
        if (!_isPuckUpHealthBooster)
        {
            GetComponent<Collider2D>().enabled = false;
            enabled = false;
            StartCoroutine(DieTick(0.5f));
        }
        else
        {
            StartCoroutine(InvisibleTick());
        }
    }

    public void NextLevel(Vector2 position, float time)
    {
        _spumPrefab.PlayAnimation(1, time);
        transform.DOMove(position, time);
        Vector3 cameraNewPosition = new Vector3(_mainCamera.transform.position.x, 
            _mainCamera.transform.position.y - 10,
            _mainCamera.transform.position.z);
        _mainCamera.transform.DOMove(cameraNewPosition, time);
    }

    private IEnumerator InvisibleTick()
    {
        yield return new WaitForSeconds(_invisibleTime);
        _isPuckUpHealthBooster = false;
    }

    private IEnumerator DieTick(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); 
        OnDie?.Invoke();
    }

    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(0.5f);
        _isAttacking = false;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthBooster healthBooster))
        {
            if (!_isPuckUpHealthBooster)
            {
                _isPuckUpHealthBooster = true;
                healthBooster.gameObject.SetActive(false);
            }
        }
        
        if (collision.gameObject.TryGetComponent(out LowPlayerSpeedZone lowPlayerSpeedZone))
        {
            _isPuckUpLowSpeedDebuf = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out LowPlayerSpeedZone lowZone))
        {
            _isPuckUpLowSpeedDebuf = false;
        }
    }
}
