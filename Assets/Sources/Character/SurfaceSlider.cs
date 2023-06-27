using System.Collections;
using UnityEngine;

public class SurfaceSlider : MonoBehaviour
{
    Vector3 _direction;
    public Vector3 Project(Vector3 forward)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, forward, out hit, 1f))
            return (forward - Vector3.Dot(forward, hit.normal) * hit.normal).normalized;
        else
            return forward;
    }
}
