using System.Collections;
using UnityEngine;

namespace Clones.BulletSystem
{
    public class LimitedLifetimeEffect : MonoBehaviour
    {
        [SerializeField] private float _lifeTime;

        private void Start() =>
            StartCoroutine(Timer());

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(_lifeTime);

            Destroy(gameObject);
        }
    }
}