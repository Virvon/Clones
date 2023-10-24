using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    public Vector3 Project(Vector3 forward)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, forward, out hit, 1f) && hit.transform.TryGetComponent(out PreyResource preyResource))
            return (forward - Vector3.Dot(forward, hit.normal) * hit.normal).normalized;
        else
            return forward;
    }
}
