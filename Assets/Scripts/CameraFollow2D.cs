using UnityEngine;
public class CameraFollow2D : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private string _playerTag;
    [SerializeField][Range(0.5f, 7.5f)] private float _movingSpeed = 1.5f;

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
            y = _playerTransform.position.y + 3,
            z = _playerTransform.position.z - 2,
        };
    }

    private void Update()
    {
        if (_playerTransform)
        {
            Vector3 target = new Vector3()
            {
                x = _playerTransform.position.x,
                y = _playerTransform.position.y + 3,
                z = _playerTransform.position.z - 2,
            };

            Vector3 pos = Vector3.Lerp(transform.position, target, _movingSpeed * Time.deltaTime);

            transform.position = pos;
        }
    }

}
