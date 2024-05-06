using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TreeWithEyes : MonoBehaviour
{
    private TreeEye[] _eyes;

    private void Start() =>
        _eyes = GetComponentsInChildren<TreeEye>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            SetTarget(player.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
            RemoveTarget(player.transform);
    }

    private void RemoveTarget(Transform transform)
    {
        foreach (var eye in _eyes)
            eye.RemoveTarget();
    }

    private void SetTarget(Transform target)
    {
        foreach (var eye in _eyes)
            eye.LookToTarget(target);
    }
}