using UnityEditor;
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

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, Vector3.up, _radius);    }
#endif
}
