using Clones.Services;
using System;
using UnityEngine;

namespace Clones.StateMachine
{
    [RequireComponent(typeof(Rigidbody), typeof(SurfaceSlider))]
    public class MovementState : State
    {
        [SerializeField] private float _directionOffset;

        private IInputService _input;
        private float _rotationSpeed;
        private Rigidbody _rigidbody;
        private SurfaceSlider _surfaceSlider;
        private Player _player;
        private bool _isMoved;

        private float MovementSpeed => _player.StatsProvider.GetStats().MovementSpeed;

        public event Action Started;
        public event Action Stopped;

        public void Init(IInputService inputService, Player player, float rotationSpeed)
        {
            _input = inputService;
            _rotationSpeed = rotationSpeed;
            _player = player;

            _rigidbody = GetComponent<Rigidbody>();
            _surfaceSlider = GetComponent<SurfaceSlider>();

            _input.Activated += Move;
            _input.Deactivated += Stop;
        }

        private void FixedUpdate()
        {
            if (_isMoved)
            {
                Vector3 direction = Quaternion.Euler(0, _directionOffset, 0) * new Vector3(InputServiece.Direction.x, 0, InputServiece.Direction.y);

                direction = _surfaceSlider.Project(direction.normalized);

                Vector3 offset = direction * MovementSpeed * Time.deltaTime;

                _rigidbody.MovePosition(_rigidbody.position + offset);

                RotateTo(direction);
            }
        }

        private void OnDestroy()
        {
            _input.Activated -= Move;
            _input.Deactivated -= Stop;
        }

        private void Move()
        {
            _isMoved = true;
            Started?.Invoke();
        }

        private void RotateTo(Vector3 direction)
        {
            if (direction == Vector3.zero)
                return;

            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        private void Stop()
        {
            _isMoved = false;
            Stopped?.Invoke();
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
