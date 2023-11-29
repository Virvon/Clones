using Clones.Infrastructure;
using Clones.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Clones.GameLogic
{
    public class WorldGenerator : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface _navMeshSurface;

        private IPartsFactory _partsFactory;
        private Transform _player;
        BiomeType[] _generationBiomes;
        private float _viewRadius;
        private float _destroyRadius;
        private float _cellSize;
        private HashSet<GameObject> _tilesMatrix = new();

        private float _offset => _cellSize / 2;
        private Vector3 _playerPosition => new Vector3(_player.position.x + _offset, _player.position.y, _player.position.z + _offset);

        public event Action<GameObject> TileCreated;
        public event Action<GameObject> TileDestroyed;

        private void Update()
        {
            if (_player == null)
                return;

            FillRadius(_playerPosition, _viewRadius);
            EmptyAroundRadius(_playerPosition, _destroyRadius);
        }

        public void Init(Transform player, BiomeType[] templates, float viewRadius, float destroyRadius, float cellSize)
        {
            _generationBiomes = templates;
            _viewRadius = viewRadius;
            _destroyRadius = destroyRadius;
            _cellSize = cellSize;
            _player = player;
        }

        public void Init(IPartsFactory partsFacotry) =>
            _partsFactory = partsFacotry;

        private void FillRadius(Vector3 center, float viewRadius)
        {
            var cellsCountOnAxis = (int)(viewRadius / _cellSize);
            var fillAreaCenter = WorldToGridPosition(center);

            for (int x = -cellsCountOnAxis; x < cellsCountOnAxis; x++)
            {
                for (int z = -cellsCountOnAxis; z < cellsCountOnAxis; z++)
                {
                    TryCreate(fillAreaCenter + new Vector3Int(x, (int)transform.position.y, z));
                }
            }
        }

        private void EmptyAroundRadius(Vector3 center, float viewRadius)
        {
            var cellsCountOnAxis = (int)(viewRadius / _cellSize);
            var fillAreaCenter = WorldToGridPosition(center);

            HashSet<GameObject> removeTileMatrix = new();

            foreach (var tile in _tilesMatrix)
            {
                Vector3Int tileGridPosition = WorldToGridPosition(tile.transform.position);
                Vector3Int upBorder = fillAreaCenter + new Vector3Int(cellsCountOnAxis, (int)transform.position.y, cellsCountOnAxis);
                Vector3Int downBorder = fillAreaCenter - new Vector3Int(cellsCountOnAxis, (int)transform.position.y, cellsCountOnAxis);

                if ((tileGridPosition.x > upBorder.x || tileGridPosition.x < downBorder.x) || (tileGridPosition.z > upBorder.z || tileGridPosition.z < downBorder.z))
                    removeTileMatrix.Add(tile);
            }

            Remove(removeTileMatrix);
        }

        private void Remove(HashSet<GameObject> tilesMatrix)
        {
            foreach (var tile in tilesMatrix)
            {
                TileDestroyed?.Invoke(tile);
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

            GameObject tileObject = _partsFactory.CreateTile(template, position, Quaternion.identity, transform);

            _navMeshSurface.BuildNavMesh();

            TileCreated?.Invoke(tileObject);
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
                (int)((worldPosition.x) / _cellSize),
                (int)((worldPosition.y) / _cellSize),
                (int)((worldPosition.z) / _cellSize));
        }
    }
}