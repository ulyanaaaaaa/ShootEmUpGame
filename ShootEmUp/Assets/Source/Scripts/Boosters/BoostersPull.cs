using System.Collections.Generic;
using UnityEngine;

public class BoostersPull : MonoBehaviour
{
    [SerializeField] private GameObject[] _boostersPrefabs; 
    private List<GameObject> _pool = new List<GameObject>(); 

    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in _pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        
        int randomIndex = Random.Range(0, _boostersPrefabs.Length);
        GameObject newObj = Instantiate(_boostersPrefabs[randomIndex]);
        newObj.SetActive(false);
        _pool.Add(newObj);
        return newObj;
    }
}
