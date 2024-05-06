using Clones.Biomes;
using System;
using Random = UnityEngine.Random;
using UnityEngine;

public class BiomeEffects : MonoBehaviour 
{
    [SerializeField] private Biome _biome;
    [SerializeField, Range(0, 100)] private float _spawnChancePercent;

    private bool _isSuccessToSpawn;

    public bool EffectIsPlayed { get; private set; }
    public Biome Biome => _biome;

    public event Action EffectStateChanged;

    private void OnEnable()
    {
        _isSuccessToSpawn = Random.Range(0, 101) < _spawnChancePercent;

        if(_isSuccessToSpawn)
        {
            _biome.PlayerEntered += OnPlayerEntered;
            _biome.PlayerExited += OnPlayerExited;
        }
    }

    private void OnDisable()
    {
        if (_isSuccessToSpawn)
        {
            _biome.PlayerEntered -= OnPlayerEntered;
            _biome.PlayerExited -= OnPlayerExited;
        }
    }

    private void OnPlayerEntered(Biome biome)
    {
        EffectIsPlayed = true;

        EffectStateChanged?.Invoke();
    }

    private void OnPlayerExited()
    {
        EffectIsPlayed = false;

        EffectStateChanged?.Invoke();
    }
}
