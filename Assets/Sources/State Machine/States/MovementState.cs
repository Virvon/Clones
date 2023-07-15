using UnityEngine;

namespace Clones.StateMachine
{
    [RequireComponent(typeof(Rigidbody), typeof(SurfaceSlider))]
    public class MovementState : State
    {
        [SerializeField] private float _movementSpeed = 10;
        [SerializeField] private float _rotationSpeed = 1080;
        [SerializeField] private float _directionOffset;

        private Rigidbody _rigidbody;
        private SurfaceSlider _surfaceSlider;

        private void OnEnable()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _surfaceSlider = GetComponent<SurfaceSlider>();

            DirectionHandler.Activated += Move;
            DirectionHandler.Deactivated += Stop;
        }

        private void OnDisable()
        {
            DirectionHandler.Activated -= Move;
            DirectionHandler.Deactivated -= Stop;
        }

        private void Move()
        {
            Vector3 direction = Quaternion.Euler(0, _directionOffset, 0) * new Vector3(DirectionHandler.Direction.x, 0, DirectionHandler.Direction.y);

            direction = _surfaceSlider.Project(direction.normalized);

            Vector3 offset = direction * _movementSpeed * Time.deltaTime;

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
