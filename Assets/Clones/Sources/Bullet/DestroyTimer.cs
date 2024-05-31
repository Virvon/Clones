using System.Collections;
using UnityEngine;

namespace Clones.DestroySystem
{
    public class DestroyTimer : MonoBehaviour
    {
        [SerializeField] private float _time;

        public virtual void Destroy() =>
            StartCoroutine(Destroyer());

        private IEnumerator Destroyer()
        {
            yield return new WaitForSeconds(_time);

            Destroy(gameObject);
        }
    }
}