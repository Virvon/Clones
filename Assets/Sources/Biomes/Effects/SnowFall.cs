using Clones.Biomes;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Biome))]
public class SnowFall : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _snowFallsPrefabs;

    [SerializeField, Range(0, 100)] private float _spawnChancePercent;
    [SerializeField] private float _destroyTime;

    private bool _isSpawned;
    private Biome _biome;
    private ParticleSystem _currentSnowFall;
    private Coroutine _snowFallDestroyer;

    private void OnEnable()
    {
        _isSpawned = Random.Range(0, 101) < _spawnChancePercent;
        _biome = GetComponent<Biome>();

        _biome.PlayerEntered += OnPlayerEntered;
        _biome.PlayerExited += OnPlayerExited;
    }

    private void OnDisable()
    {
        _biome.PlayerEntered -= OnPlayerEntered;
        _biome.PlayerExited -= OnPlayerExited;
    }

    private void OnPlayerEntered(Biome biome)
    {
        if (_isSpawned == false || _snowFallsPrefabs.Length == 0)
            return;

        if (_currentSnowFall != null)
        {
            if (_snowFallDestroyer != null)
                StopCoroutine(SnowFallDestroyer());

            Destroy(_currentSnowFall.gameObject);
        }
    
        _currentSnowFall = Instantiate(_snowFallsPrefabs[Random.Range(0, _snowFallsPrefabs.Length)], biome.Player.transform.position, biome.Player.transform.rotation, biome.Player.transform);
    }

    private void OnPlayerExited()
    {
        if (_snowFallDestroyer != null)
            StopCoroutine(_snowFallDestroyer);

        _snowFallDestroyer = StartCoroutine(SnowFallDestroyer());
    }

    private IEnumerator SnowFallDestroyer()
    {
        _currentSnowFall.Stop();

        yield return new WaitForSeconds(_destroyTime);

        Destroy(_currentSnowFall.gameObject);
    }
}
