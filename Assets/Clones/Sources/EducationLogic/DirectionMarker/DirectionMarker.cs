using UnityEngine;

namespace Clones.EducationLogic
{
    public class DirectionMarker : MonoBehaviour
    {
        [SerializeField] private float _disableDistance;

        private Transform _target;
        private Transform _player;

        public void Init(Transform player)
        {
            _player = player;
            gameObject.SetActive(false);
        }

        private void LateUpdate()
        {
            if (_player == null || _target == null)
                return;

            float distance = Vector3.Distance(_target.position, _player.position);

            if (distance <= _disableDistance)
                gameObject.SetActive(false);

            Vector3 direction = (_target.position - _player.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(Vector3.up, direction);

            transform.rotation = rotation;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
            gameObject.SetActive(true);
        }
    }
}
