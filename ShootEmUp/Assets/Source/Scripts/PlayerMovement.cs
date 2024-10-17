using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private SPUM_Prefabs spumPrefab;
    private Vector2 movement;
    private bool isAttacking = false;
    private bool facingRight = false;

    private void Awake()
    {
        spumPrefab = GetComponent<SPUM_Prefabs>();
    }

    private void Update()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement.sqrMagnitude > 0.1f)
        {
            spumPrefab.PlayAnimation(1); 

            if (movement.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (movement.x < 0 && facingRight)
            {
                Flip();
            }
        }
        else
        {
            spumPrefab.PlayAnimation(0); 
        }
        
        Attack();
        Move();
    }

    private void Move()
    {
        transform.Translate(movement * _moveSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            spumPrefab.PlayAnimation(4);
            StartCoroutine(EndAttack());
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(0.5f); 
        isAttacking = false;
    }
}
