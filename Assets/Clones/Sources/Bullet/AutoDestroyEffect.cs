using System.Collections;
using UnityEngine;

namespace Clones.BulletSystem
{
    [RequireComponent(typeof(ParticleSystem))]
    public class AutoDestroyEffect : MonoBehaviour
    {
        private void Start() =>
            StartCoroutine(Timer());

        private IEnumerator Timer()
        {
            var particleSystem = GetComponent<ParticleSystem>();

            yield return new WaitWhile(particleSystem.IsAlive);

            Destroy(gameObject);
        }
    }
}