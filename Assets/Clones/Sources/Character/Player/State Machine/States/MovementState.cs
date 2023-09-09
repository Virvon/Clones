using Clones.Biomes;
using Clones.Infrastructure;
using System;
using UnityEngine;

namespace Clones.StateMachine
{
    [RequireComponent(typeof(Rigidbody), typeof(SurfaceSlider))]
    public class MovementState : State
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _rotationSpeed = 1080;
        [SerializeField] private float _directionOffset;

        private Rigidbody _rigidbody;
        private SurfaceSlider _surfaceSlider;
        private IInputService _input;
        private IMovementSpeedChanger _movementSpeedChanger;

        public float MaxMovementSpeed => _movementSpeed;
        public float MovementSpeed => _movementSpeedChanger != null ? _movementSpeedChanger.MovementSpeed : _movementSpeed;

        public event Action MovementSpeedChanged;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _surfaceSlider = GetComponent<SurfaceSlider>();
            _input = AllServices.Instance.Single<IInputService>(); 

            _input.Activated += Move;
            _input.Deactivated += Stop;
        }

        private void OnDisable()
        {
            _input.Activated -= Move;
            _input.Deactivated -= Stop;
        }

        public void GetMovementSpeedChanger(IMovementSpeedChanger movementSpeedChanger) => 
            _movementSpeedChanger = movementSpeedChanger;

        private void Move()
        {
            Vector3 direction = Quaternion.Euler(0, _directionOffset, 0) * new Vector3(InputServiece.Direction.x, 0, InputServiece.Direction.y);

            direction = _surfaceSlider.Project(direction.normalized);

            Vector3 offset = direction * MovementSpeed * Time.deltaTime;

            _rigidbody.MovePosition(_rigidbody.position + offset);

            RotateTo(direction);
        }

        private void RotateTo(Vector3 direction)
        {
            if (direction == Vector3.zero)
                return;

            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        private void Stop() => _rigidbody.velocity = Vector3.zero;
    }
}
