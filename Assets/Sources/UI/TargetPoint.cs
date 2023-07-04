using UnityEngine;
using Clones.StateMachine;

namespace Clones.UI
{
    public class TargetPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _pointPrefab;
        [SerializeField] private AttackState[] _attackStates;
        [SerializeField] private Vector3 _pointOffset;

        private GameObject _point;
        private ITargetable _target;

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

        private void Update()
        {
            if (_target == null)
                return;

            _point.SetActive(true);
            _point.transform.position = _target.Position + _pointOffset;
        }

        private void InitPoint()
        {
            _point = Instantiate(_pointPrefab);
            _point.SetActive(false);
        }

        private void OnTargetSelected(ITargetable target) => _target = target;

        private void OnTargetRejected()
        {
            _target = null;
            _point.SetActive(false);
        }
    }
}
