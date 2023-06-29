using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefabList
{
    public string ListName;
    [Range(0, 100)]
    public int PercentageFilled;
    public List<GameObject> PrefabsToGenerate;
    public List<Transform> SpawnPoints;
    [HideInInspector] public List<GameObject> SpawnedPrefabs;
}

public class PrefabGenerator : MonoBehaviour
{
    [SerializeField]
    private List<PrefabList> _prefabLists = new List<PrefabList>();

    private const int Percent = 100;

    private void Start()
    {
        GeneratePrefabs();
    }

    private void GeneratePrefabs()
    {
        foreach (PrefabList list in _prefabLists)
        {
            int pointsToGenerate = Mathf.RoundToInt((float)list.PercentageFilled / Percent * list.SpawnPoints.Count);

            for (int i = 0; i < pointsToGenerate; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, list.PrefabsToGenerate.Count);
                GameObject prefabToGenerate = list.PrefabsToGenerate[randomIndex];
                Transform spawnPoint = GetRandomSpawnPoint(list.SpawnPoints);

                if (prefabToGenerate != null && spawnPoint != null)
                {
                    GameObject spawnedPrefab = Instantiate(prefabToGenerate, spawnPoint.position, spawnPoint.rotation);
                    list.SpawnedPrefabs.Add(spawnedPrefab);

                    spawnedPrefab.transform.SetParent(transform);
                }
            }
        }
    }

    private Transform GetRandomSpawnPoint(List<Transform> spawnPoints)
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("No spawn points available.");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex];
    }

    public void DeleteSpawnedPrefabs(GameObject tile)
    {
        foreach (PrefabList list in _prefabLists)
        {
            for (int i = list.SpawnedPrefabs.Count - 1; i >= 0; i--)
            {
                GameObject spawnedPrefab = list.SpawnedPrefabs[i];

                if (spawnedPrefab != null && spawnedPrefab.transform.IsChildOf(tile.transform))
                {
                    Destroy(spawnedPrefab);
                    list.SpawnedPrefabs.RemoveAt(i);
                }
            }
        }
    }
}
