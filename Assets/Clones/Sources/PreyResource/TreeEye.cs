using System.Collections;
using UnityEngine;

public class TreeEye : MonoBehaviour
{
    [SerializeField] private float _maxRotationAngle;
    [SerializeField] private float _rotationSpeed;

    private bool _hasTarget;
    private Coroutine _rotation;
    private float _horizontalOffset;
    private float _verticalOffset;

    private void Start()
    { 
        _horizontalOffset = transform.rotation.eulerAngles.y;
        _verticalOffset = transform.rotation.eulerAngles.x;
    }

    public void LookToTarget(Transform target)
    {
        if (_rotation != null)
            StopCoroutine(_rotation);

        _hasTarget = true;
        _rotation = StartCoroutine(Looker(target));
    }

    public void RemoveTarget() => 
        _hasTarget = false;

    private void ClampHorizontalRotation()
    {
        if (transform.rotation.eulerAngles.y > _horizontalOffset + _maxRotationAngle)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, _horizontalOffset + _maxRotationAngle, transform.rotation.eulerAngles.z);
        else if (transform.rotation.eulerAngles.y < _horizontalOffset - _maxRotationAngle)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, _horizontalOffset - _maxRotationAngle, transform.rotation.eulerAngles.z);
    }

    private void ClampVerticalRotation()
    {
        if (transform.rotation.eulerAngles.x > _verticalOffset + _maxRotationAngle)
            transform.rotation = Quaternion.Euler(_verticalOffset + _maxRotationAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        else if (transform.rotation.eulerAngles.x < _verticalOffset - _maxRotationAngle)
            transform.rotation = Quaternion.Euler(_verticalOffset - _maxRotationAngle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void Rotate(Transform target)
    {
        Quaternion rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
    }

    private IEnumerator Looker(Transform target)
    {
        while (_hasTarget)
        {
            Rotate(target);
            ClampHorizontalRotation();
            ClampVerticalRotation();

            yield return null;
        }

        Quaternion defaultRotation = Quaternion.Euler(_verticalOffset, _horizontalOffset, 0);
        float time = 0;

        while (transform.rotation != defaultRotation)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation, defaultRotation, time );

            yield return null;
        }
    }
}