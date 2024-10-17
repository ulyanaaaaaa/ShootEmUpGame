using UnityEngine;

public class Cartridge : MonoBehaviour
{
    private ShootPull _shootPull;
    private Vector2 direction;
    
    public void Setup(ShootPull shootPull)
    {
        _shootPull = shootPull;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.Die();
            Die();
        }
    }
    
    private void Die()
    {
        gameObject.SetActive(false);
        _shootPull.SpawnCartridge();
    }
}
