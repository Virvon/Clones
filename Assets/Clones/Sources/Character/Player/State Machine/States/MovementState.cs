using Clones.Services;
using System;
using UnityEngine;

namespace Clones.StateMachine
{
    [RequireComponent(typeof(Rigidbody), typeof(SurfaceSlider))]
    public class MovementState : State
    {
        [SerializeField] private float _directionOffset;

        private float _movementSpeed;
        private float _rotationSpeed;
        private Rigidbody _rigidbody;
        private SurfaceSlider _surfaceSlider;
        private IMovementSpeedChanger _movementSpeedChanger;

        public float MaxMovementSpeed { get; private set; }
        public float MovementSpeed => _movementSpeedChanger != null ? _movementSpeedChanger.MovementSpeed : _movementSpeed;

        public event Action MovementSpeedChanged;

        private void OnEnable()
        {
            InputServiece.Activated += Move;
            InputServiece.Deactivated += Stop;
        }

        private void OnDisable()
        {
            InputServiece.Activated -= Move;
            InputServiece.Deactivated -= Stop;
        }

        public void Init(float movementSpeed, float rotationSpeed)
        {
            _movementSpeed = movementSpeed;
            _rotationSpeed = rotationSpeed;
            MaxMovementSpeed = _movementSpeed;

            _rigidbody = GetComponent<Rigidbody>();
            _surfaceSlider = GetComponent<SurfaceSlider>();
        }

        public void SetMovementSpeedChanger(IMovementSpeedChanger movementSpeedChanger)
        {
            _movementSpeedChanger = movementSpeedChanger;
            _movementSpeedChanger.MovementSpeedChanged += MovementSpeedChanged;
        }

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
