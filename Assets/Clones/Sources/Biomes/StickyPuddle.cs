using Clones.StateMachine;
using System;
using UnityEngine;

namespace Clones.Biomes
{
    [RequireComponent(typeof(Collider))]
    public class StickyPuddle : MonoBehaviour, IMovementSpeedChanger
    {
        [SerializeField, Range(0, 100)] private int _movementSpeedPercent;

        private MovementState _movementState;
        private float _movementSpeed;

        public float MovementSpeed 
        {
            get
            {
                return _movementSpeed;
            } 
            private set
            {
                _movementSpeed = value;
                MovementSpeedChanged?.Invoke();
            }
        }

        public event Action MovementSpeedChanged;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _movementState = player.GetComponent<MovementState>();
                _movementState.SetMovementSpeedChanger(this);

                MovementSpeed = _movementState.MaxMovementSpeed * (_movementSpeedPercent / 100f);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                MovementSpeed = _movementState.MaxMovementSpeed;
        }
    }
}
