using System.Collections.Generic;
using UnityEngine;

public class ShootPull : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint; 
    private Cartridge _cartridge; 
    [SerializeField] private int _initialPoolSize = 20;
    private List<Cartridge> _pool = new List<Cartridge>();

    private void Awake()
    {
        _cartridge = Resources.Load<Cartridge>("Cartridge");
        
        for (int i = 0; i < _initialPoolSize; i++)
        {
            Cartridge cartridge = Instantiate(_cartridge);
            cartridge.gameObject.SetActive(false);
            _pool.Add(cartridge);
        }
    }

    public Cartridge GetPooledCartridge()
    {
        foreach (Cartridge cartridge in _pool)
        {
            if (!cartridge.gameObject.activeInHierarchy)
            {
                return cartridge;
            }
        }
        
        Cartridge newCartridge = Instantiate(_cartridge);
        newCartridge.gameObject.SetActive(false);
        _pool.Add(newCartridge);
        return newCartridge;
    }

    public void SpawnCartridge(Vector2 direction)
    {
        Cartridge cartridge = GetPooledCartridge();
        cartridge.transform.position = _spawnPoint.position;
        cartridge.SetDirection(direction);
        cartridge.gameObject.SetActive(true);
    }
}
