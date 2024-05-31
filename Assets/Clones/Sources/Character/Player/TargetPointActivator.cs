using UnityEngine;
using Clones.StateMachine;

namespace Clones.Character.Player
{
    public class TargetPointActivator : MonoBehaviour
    {
        private AttackState[] _attackStates;
        private TargetPoint _currentPoint;

        private void OnEnable()
        {
            _attackStates = GetComponents<AttackState>();

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

        private void OnTargetSelected(GameObject target)
        {
            TargetPoint targetPoint = target.GetComponentInChildren<TargetPoint>();

            if (targetPoint != null)
            {
                _currentPoint = targetPoint;
                targetPoint.Active();
            }
        }

        private void OnTargetRejected()
        {
            if (_currentPoint != null)
            {
                _currentPoint.Deactive();
                _currentPoint = null;
            }
        }
    }
}