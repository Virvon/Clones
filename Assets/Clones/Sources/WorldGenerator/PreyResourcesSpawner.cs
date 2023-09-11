using Clones.Data;
using UnityEngine;

public class PreyResourcesSpawner : MonoBehaviour
{
    [SerializeField] private BiomeData _biomeData;
    [SerializeField, Range(0, 100)] private int _percentageFilled;

    private Point[] _spawnPoints;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<Point>();
    }
}