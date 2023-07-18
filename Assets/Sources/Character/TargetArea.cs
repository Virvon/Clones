using UnityEngine;

public class TargetArea : MonoBehaviour
{
    [SerializeField] private Clone _target;
    [SerializeField] private float _radius;

    private void Start()
    {
        transform.position = _target.transform.position;
    }

    private void Update()
    {
        if((transform.position - _target.transform.position).magnitude > _radius)
            transform.position = _target.transform.position;
    }
}
