using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10;
    [SerializeField] private float _rotationSpeed = 1080;
    [SerializeField] private float _directionOffset;
    [SerializeField] private DirectionHandler _directionHandler;

    private Rigidbody _rigidbody;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _directionHandler.Activated += Move;
        _directionHandler.Deactivated += Stop;
    }

    private void OnDisable()
    {
        _directionHandler.Activated -= Move;
        _directionHandler.Deactivated -= Stop;
    }

    private void Move()
    {
        Vector3 direction = Quaternion.Euler(0, _directionOffset, 0) * new Vector3(_directionHandler.Direction.x, 0, _directionHandler.Direction.y);

        _rigidbody.velocity = direction * _movementSpeed;

        RotateTo(direction);
    }

    private void RotateTo(Vector3 direction)
    {
        var targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void Stop() => _rigidbody.velocity = Vector3.zero;
}
