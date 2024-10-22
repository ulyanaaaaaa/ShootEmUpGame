using System.Collections;
using UnityEngine;

public class Booster : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    private Coroutine _lifeTick;

    private void OnEnable()
    {
        _lifeTick = StartCoroutine(LifeTick());
    }

    private IEnumerator LifeTick()
    {
        yield return new WaitForSeconds(_lifeTime);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopCoroutine(_lifeTick);
    }
}
