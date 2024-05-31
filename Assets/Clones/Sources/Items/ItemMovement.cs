using System.Collections;
using UnityEngine;

namespace Clones.Items
{
    public class ItemMovement : MonoBehaviour
    {
        private Coroutine _mover;

        public void TakeMove(Vector3 targetPosition, float speed)
        {
            if (_mover != null)
                StopCoroutine(_mover);

            _mover = StartCoroutine(Mover(targetPosition, speed));
        }

        public void TakeMove(Transform targetTransform, float speed)
        {
            if (_mover != null)
                StopCoroutine(_mover);

            _mover = StartCoroutine(Mover(targetTransform, speed));
        }

        private IEnumerator Mover(Vector3 targetPosition, float speed)
        {
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);

                yield return null;
            }
        }

        private IEnumerator Mover(Transform targetTransform, float speed)
        {
            while (transform.position != targetTransform.position)
            {
                transform.position = Vector3.Lerp(transform.position, targetTransform.position, speed * Time.deltaTime);

                yield return null;
            }
        }
    }
}