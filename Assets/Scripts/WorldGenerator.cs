using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private List<GameObject> _tilePrefabs;
    [SerializeField] private float _generationRadius;
    [SerializeField] private float _deactivationRadius;
    [SerializeField] private int _tilePoolSize;
    [SerializeField] private float _tileSize;
    [SerializeField] private float _updateInterval;

    private List<GameObject> _tilePool;
    private Dictionary<Vector3, GameObject> _activeTiles;
    private Queue<GameObject> _inactiveTiles = new Queue<GameObject>();
    private float _timer;
    private bool _canUpdateTiles = true;

    private const float CountRotateOptionsTile = 4f;
    private const float AngleStepRotateTile = 90f;

    private void Start()
    {
        _tilePool = new List<GameObject>(_tilePoolSize);
        _activeTiles = new Dictionary<Vector3, GameObject>(_tilePoolSize);

        for (int i = 0; i < _tilePoolSize; i++)
        {
            GameObject newTile = Instantiate(_tilePrefabs[Random.Range(0, _tilePrefabs.Count)], transform);
            newTile.SetActive(false);
            _tilePool.Add(newTile);
            _inactiveTiles.Enqueue(newTile);
        }

        GenerateTiles();
    }

    private void FixedUpdate()
    {
        if (!_canUpdateTiles)
            return;

        _timer += Time.deltaTime;

        if (_timer >= _updateInterval)
        {
            GenerateTiles();
            DeactivateTiles();
            _timer = 0f;
        }
    }

    private void GenerateTiles()
    {
        Vector3 currentPlayerTilePosition = WorldToTilePosition(_playerTransform.position);
        int horizontalTiles = Mathf.CeilToInt(_generationRadius / _tileSize);
        int verticalTiles = Mathf.CeilToInt(_generationRadius / _tileSize);

        for (int x = -horizontalTiles; x <= horizontalTiles; x++)
        {
            for (int z = -verticalTiles; z <= verticalTiles; z++)
            {
                Vector3 tilePosition = currentPlayerTilePosition + new Vector3(x * _tileSize, 0, z * _tileSize);

                if (!_activeTiles.ContainsKey(tilePosition))
                {
                    GameObject newTile = GetTileFromPool();
                    PlaceTile(newTile, tilePosition);
                    _activeTiles.Add(tilePosition, newTile);
                }
            }
        }
    }

    private void DeactivateTiles()
    {
        int horizontalTiles = Mathf.CeilToInt(_deactivationRadius / _tileSize);
        int verticalTiles = Mathf.CeilToInt(_deactivationRadius / _tileSize);

        List<Vector3> tilesToDeactivate = new List<Vector3>();

        foreach (KeyValuePair<Vector3, GameObject> tileEntry in _activeTiles)
        {
            Vector3 tilePosition = tileEntry.Key;

            if (Mathf.Abs(tilePosition.x - _playerTransform.position.x) > horizontalTiles * _tileSize ||
                Mathf.Abs(tilePosition.z - _playerTransform.position.z) > verticalTiles * _tileSize)
            {
                tilesToDeactivate.Add(tilePosition);
            }
        }

        foreach (Vector3 tilePosition in tilesToDeactivate)
        {
            GameObject tileToDeactivate = _activeTiles[tilePosition];
            _activeTiles.Remove(tilePosition);
            ReturnTileToPool(tileToDeactivate);
        }
    }

    private GameObject GetTileFromPool()
    {
        if (_inactiveTiles.Count > 0)
            return _inactiveTiles.Dequeue();

        GameObject newTile = Instantiate(_tilePrefabs[Random.Range(0, _tilePrefabs.Count)], transform);
        _tilePool.Add(newTile);
        return newTile;
    }

    private void ReturnTileToPool(GameObject tile)
    {
        tile.SetActive(false);
        _inactiveTiles.Enqueue(tile);
    }

    private void PlaceTile(GameObject tile, Vector3 position)
    {
        tile.transform.position = position;
        tile.SetActive(true);
        tile.transform.rotation = Quaternion.Euler(0f, Random.Range(0, CountRotateOptionsTile) * AngleStepRotateTile, 0f);
    }

    private Vector3 WorldToTilePosition(Vector3 worldPosition)
    {
        return new Vector3(
            Mathf.Floor(worldPosition.x / _tileSize) * _tileSize,
            0f,
            Mathf.Floor(worldPosition.z / _tileSize) * _tileSize
        );
    }
    public void SetTileUpdatesEnabled(bool enabled)
    {
        _canUpdateTiles = enabled;
    }
}
