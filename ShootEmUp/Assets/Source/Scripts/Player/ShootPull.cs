using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPull : MonoBehaviour
{
    [SerializeField] private int _poolSize = 20; 
    [SerializeField] private Transform _spawnPoint; 
    private Cartridge _cartridge; 
    private List<GameObject> _pool;

    private void Awake()
    {
        _cartridge = Resources.Load<Cartridge>("Cartridge");
    }

    private void Start()
    {
        _pool = new List<GameObject>();
        for (int i = 0; i < _poolSize; i++)
        {
            Cartridge cartridge = Instantiate(_cartridge);
            cartridge.gameObject.SetActive(false);
            _pool.Add(cartridge.gameObject);
        }
    }

    private GameObject GetPooledObject()
    {
        foreach (GameObject obj in _pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        
        Cartridge cartridge = Instantiate(_cartridge);
        cartridge.Setup(GetComponent<ShootSpawner>().ShootPull);
        cartridge.gameObject.SetActive(false);
        _pool.Add(cartridge.gameObject);
        return cartridge.gameObject;
    }

    public void SpawnCartridge()
    {
        GameObject pooledObject = GetPooledObject();
        if (pooledObject != null)
        {
            pooledObject.transform.position = _spawnPoint.position;
            pooledObject.SetActive(true);
        }
    }
}
