using UnityEngine;
using Clones.StateMachine;

namespace Clones.UI
{
    public class SelectedToAttackTarget : MonoBehaviour
    {
        [SerializeField] private GameObject _pointPrefab;
        [SerializeField] private AttackState[] _attackStates;
        [SerializeField] private float _pointVerticalOffset;

        private GameObject _point;
        private Vector3 _targetPosition;

        private void OnEnable()
        {
            InitPoint();

            foreach (var state in _attackStates)
            {
                state.TargetSelected += OnTargetSelected;
                state.TargetRejected += OnTargetRejected;
            }
        }

        private void OnDisable()
        {
            foreach (var state in _attackStates)
            {
                state.TargetSelected -= OnTargetSelected;
                state.TargetRejected -= OnTargetRejected;
            }
        }

        private void LateUpdate()
        {
            if (_targetPosition == null)
                return;

            _point.transform.position = new Vector3(_targetPosition.x, _pointVerticalOffset, _targetPosition.z);
        }

        private void InitPoint()
        {
            _point = Instantiate(_pointPrefab);
            _point.SetActive(false);
        }

        private void OnTargetSelected(Transform targetTransform)
        {
            _targetPosition = targetTransform.position;
            _point.SetActive(true);
        }

        private void OnTargetRejected()
        {
            if(_point != null )
                _point.SetActive(false);

            _targetPosition = Vector3.zero;
        }
    }
}
