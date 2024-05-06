using UnityEngine;

namespace Clones.StateMachine
{
    public class TargetPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _point;
        [SerializeField] private float _rotationSpeed = 120;

        private bool _isTarget;

        private void Start()
        {
            _point.SetActive(false);
            _isTarget = false;
        }

        private void Update()
        {
            if(_isTarget)
                _point.transform.Rotate(Vector3.back, _rotationSpeed * Time.deltaTime);
        }

        public void Active()
        {
            _point.SetActive(true);
            _isTarget = true;
        }

        public void Deactive()
        {
            _point.SetActive(false);
            _isTarget = false;
        }
    }
}