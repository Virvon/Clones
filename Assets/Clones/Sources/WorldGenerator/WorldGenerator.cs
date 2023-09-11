using Clones.Infrastructure;
using Clones.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    private IGameFactory _gameFactory;
    private Transform _player;
    BiomeType[] _generationBiomes;
    private float _viewRadius;
    private float _cellSize;
    private HashSet<GameObject> _tilesMatrix = new();

    private void Update()
    {
        if (_player == null)
            return;

        FillRadius(_player.position, _viewRadius);
        EmptyAroundRadius(_player.position, _viewRadius);
    }

    public void Init(IGameFactory gameFactory, Transform player, BiomeType[] templates, float viewRadius, float cellSize)
    {
        _gameFactory = gameFactory;
        _generationBiomes = templates;
        _viewRadius = viewRadius;
        _cellSize = cellSize;
        _player = player;
    }

    private void FillRadius(Vector3 center, float viewRadius)
    {
        var cellCountOnAxis = (int)(viewRadius / _cellSize);
        var fillAreaCenter = WorldToGridPosition(center);

        

        for (int x = -cellCountOnAxis; x < cellCountOnAxis; x++)
        {
            for (int z = -cellCountOnAxis; z < cellCountOnAxis; z++)
            {
                TryCreate(fillAreaCenter + new Vector3Int(x, (int)transform.position.y, z));
            }
        }
    }

    private void EmptyAroundRadius(Vector3 center, float viewRadius)
    {
        HashSet<GameObject> removeTileMatrix = new();

        foreach(var tile in _tilesMatrix)
        {
            if (Vector3.Distance(center, tile.transform.position) > viewRadius)
                removeTileMatrix.Add(tile);
        }

        Remove(removeTileMatrix);
    }

    private void Remove(HashSet<GameObject> tilesMatrix)
    {
        foreach(var tile in tilesMatrix)
        {
            _tilesMatrix.Remove(tile);
            Destroy(tile);
        }
    }

    private void TryCreate(Vector3Int gridPosition)
    {
        gridPosition.y = (int)transform.position.y;

        if (_tilesMatrix.Any(tile => WorldToGridPosition(tile.transform.position) == gridPosition))
            return;


        var template = GetRandomBiomeType();

        var position = GridToWorldPosition(gridPosition);

        GameObject tileObject = _gameFactory.CreateTile(template, position, Quaternion.identity, transform);

        _tilesMatrix.Add(tileObject);
    }

    private BiomeType GetRandomBiomeType() => 
        _generationBiomes[Random.Range(0, _generationBiomes.Length)];

    private Vector3 GridToWorldPosition(Vector3Int gridPosition)
    {
        return new Vector3(
            gridPosition.x * _cellSize,
            gridPosition.y * _cellSize,
            gridPosition.z * _cellSize);
    }

    private Vector3Int WorldToGridPosition(Vector3 worldPosition)
    {
        return new Vector3Int(
            (int)(worldPosition.x / _cellSize),
            (int)(worldPosition.y / _cellSize),
            (int)(worldPosition.z / _cellSize));
    }
}