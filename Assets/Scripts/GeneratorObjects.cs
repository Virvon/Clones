using Clones.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorObjects : MonoBehaviour
{
    [SerializeField ,Range(0, 100)] private int _percentageFilled;
    [SerializeField] private BiomeData _biomeData;

    private const int Percent = 100;

    private List<PreyResource> _preyResources = new List<PreyResource>();
    private Point[] _spawnPoints;
    private List<PreyResource> _prefabsToGenerate;
    private List<PreyResource> _spawnedPrefabs = new List<PreyResource>();

    public List<PreyResource> PreyResources => _preyResources;

    private void Awake()
    {
        foreach(var preyResourceData in _biomeData.PreyResourcesDatas)
            _prefabsToGenerate.Add(preyResourceData.PreyResourcePrefab);

        _spawnPoints = GetComponentsInChildren<Point>();
        GeneratePrefabs();
    }

    private void GeneratePrefabs()
    {
        int pointsToGenerate = Mathf.RoundToInt((float)_percentageFilled / Percent * _spawnPoints.Length);

        List<Point> availableSpawnPoints = new List<Point>(_spawnPoints);

        for (int i = 0; i < pointsToGenerate; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, _prefabsToGenerate.Count);
            PreyResource prefabToGenerate = _prefabsToGenerate[randomIndex];

            if (availableSpawnPoints.Count == 0)
            {
                Debug.LogWarning("No more available spawn points.");
                break;
            }

            int spawnIndex = UnityEngine.Random.Range(0, availableSpawnPoints.Count);
            Point spawnPoint = availableSpawnPoints[spawnIndex];
            availableSpawnPoints.RemoveAt(spawnIndex);

            if (prefabToGenerate != null && spawnPoint != null)
            {
                PreyResource spawnedPrefab = Instantiate(prefabToGenerate, spawnPoint.transform.position, spawnPoint.transform.rotation);
                _spawnedPrefabs.Add(spawnedPrefab);
                spawnedPrefab.transform.SetParent(transform);

                if (spawnedPrefab.TryGetComponent(out PreyResource miningFacility))
                    _preyResources.Add(miningFacility);
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
        for (int i = _spawnedPrefabs.Count - 1; i >= 0; i--)
        {
            PreyResource spawnedPrefab = _spawnedPrefabs[i];

            if (spawnedPrefab != null && spawnedPrefab.transform.IsChildOf(tile.transform))
            {
                Destroy(spawnedPrefab);
                _spawnedPrefabs.RemoveAt(i);
            }
        }
    }
}