using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PrefabList
{
    public string ListName;
    [Range(0, 100)] public int PercentageFilled;
    public List<GameObject> PrefabsToGenerate;
    public List<Transform> SpawnPoints;
}

public class PrefabGenerator : MonoBehaviour
{
    [SerializeField] private List<PrefabList> _prefabLists;

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
                int randomIndex = Random.Range(0, list.PrefabsToGenerate.Count);
                GameObject prefabToGenerate = list.PrefabsToGenerate[randomIndex];
                Transform spawnPoint = GetRandomSpawnPoint(list.SpawnPoints);

                if (prefabToGenerate != null && spawnPoint != null)
                {
                    Instantiate(prefabToGenerate, spawnPoint.position, spawnPoint.rotation);
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

        int randomIndex = Random.Range(0, spawnPoints.Count);
        return spawnPoints[randomIndex];
    }
}
