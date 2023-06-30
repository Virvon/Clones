using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceData
{
    public string name;
    public GameObject prefab;
    public int minDroppedResources;
    public int maxDroppedResources;
}

public class DestructibleObject : MonoBehaviour
{
    public List<ResourceData> resources;

    private void OnDestroy()
    {
        foreach (ResourceData resource in resources)
        {
            int numResources = Random.Range(resource.minDroppedResources, resource.maxDroppedResources + 1);

            print("Выпало " + numResources + " ресурсов");

            for (int i = 0; i < numResources; i++)
            {
                GameObject resourceInstance = Instantiate(resource.prefab, transform.position, Quaternion.identity);
                Vector3 targetPosition = GetRandomTargetPosition(resourceInstance.transform);
                resourceInstance.GetComponent<ResourcePrefab>().SetTargetPosition(targetPosition);
            }
        }
    }

    private Vector3 GetRandomTargetPosition(Transform prefabTransform)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float distance = Random.Range(0.5f, 2f);
        Vector3 targetPosition = prefabTransform.position + new Vector3(randomDirection.x, 0f, randomDirection.y) * distance;
        return targetPosition;
    }
}

/*public class ResourcePrefab : MonoBehaviour
{
    public void SetTargetPosition(Vector3 targetPosition)
    {
        GetComponent<ResourcePrefabScript>().GetTargetPosition(targetPosition);
    }
}*/
