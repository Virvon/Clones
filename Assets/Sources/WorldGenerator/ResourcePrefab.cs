using UnityEngine;

public class ResourcePrefab : MonoBehaviour
{
    private Vector3 targetPosition;

    public void SetTargetPosition(Vector3 target)
    {
        targetPosition = target;
    }

    private void Update()
    {
        float speed = 5f;
        transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
