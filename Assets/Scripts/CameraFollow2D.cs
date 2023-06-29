using UnityEngine;
public class CameraFollow2D : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private string _playerTag;
    [SerializeField] private float _movingSpeed = 0.00f;
    [SerializeField] private float _visionRange;

    private void Awake()
    {
        if (_playerTransform == null)
        {
            if (_playerTag == "")
            {
                _playerTag = "Player";
            }

            _playerTransform = GameObject.FindGameObjectWithTag(_playerTag).transform;
        }

        transform.position = new Vector3()
        {
            x = _playerTransform.position.x,
            y = _playerTransform.position.y + _visionRange,
            z = _playerTransform.position.z - _visionRange,
        };
    }

    private void FixedUpdate()
    {
        if (_playerTransform)
        {
            Vector3 target = new Vector3()
            {
                x = _playerTransform.position.x,
                y = _playerTransform.position.y + _visionRange,
                z = _playerTransform.position.z - _visionRange,
            };

            Vector3 pos = Vector3.Lerp(transform.position, target, _movingSpeed * Time.deltaTime);

            transform.position = pos;
        }
    }
}
