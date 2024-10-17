using UnityEngine;

public class ShootPull : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint; 
    private Cartridge _cartridge; 

    private void Awake()
    {
        _cartridge = Resources.Load<Cartridge>("Cartridge");
    }
    
    public void SpawnCartridge(Vector2 direction)
    {
        Cartridge cartridge = Instantiate(_cartridge);
        cartridge.Setup(GetComponent<ShootSpawner>().ShootPull);
        cartridge.transform.position = _spawnPoint.position;
        cartridge.GetComponent<Cartridge>().SetDirection(direction);
    }
}
