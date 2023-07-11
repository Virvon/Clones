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

public class ResourceDrop : MonoBehaviour
{
    [SerializeField] private List<ResourceData> _resources;
    [SerializeField] private float _radiusDropResource;
    [SerializeField] private Vector3 _dropOffset;

    private void OnDestroy()
    {
        GenerateResources();
    }

    private void GenerateResources()
    {
        if(gameObject.scene.isLoaded == false) return;
        
        foreach (ResourceData resource in _resources)
        {
            int numResources = Random.Range(resource.minDroppedResources, resource.maxDroppedResources + 1);

            for (int i = 0; i < numResources; i++)
            {
                GameObject resourceInstance = Instantiate(resource.prefab, transform.position + _dropOffset, Quaternion.identity);
                Vector3 targetPosition = GetRandomTargetPosition(resourceInstance.transform);
                resourceInstance.GetComponent<ResourcePrefab>().SetTargetPosition(targetPosition);
            }
        }
    }

    private Vector3 GetRandomTargetPosition(Transform prefabTransform)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float distance = Random.Range(0, _radiusDropResource);
        Vector3 targetPosition = prefabTransform.position + new Vector3(randomDirection.x, 0f, randomDirection.y) * distance;
        return targetPosition;
    }
}
