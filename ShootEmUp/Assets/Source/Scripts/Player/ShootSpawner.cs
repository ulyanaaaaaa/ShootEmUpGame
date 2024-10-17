using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ShootPull))]
public class ShootSpawner : MonoBehaviour
{
    [SerializeField] private float _spawnInterval = 2f;
    public ShootPull ShootPull;

    private void Awake()
    {
        ShootPull = GetComponent<ShootPull>();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);
            ShootPull.SpawnCartridge();
        }
    }
}
