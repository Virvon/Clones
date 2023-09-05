using System.Collections;
using UnityEngine;

public class LimitedLifetimeEffect : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private void Start() => StartCoroutine(Timer());

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(_lifeTime);

        Destroy(gameObject);
    }
}
