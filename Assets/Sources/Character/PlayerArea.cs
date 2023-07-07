using UnityEngine;

public class PlayerArea : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _radius;

    private void Start()
    {
        transform.position = _player.transform.position;
    }

    private void Update()
    {
        if (_player == null)
            return;

        if((transform.position - _player.transform.position).magnitude > _radius)
            transform.position = _player.transform.position;
    }
}
