using Clones.Biomes;
using System;
using Random = UnityEngine.Random;
using UnityEngine;

[RequireComponent(typeof(Biome))]
public class BiomeEffects : MonoBehaviour 
{
    [SerializeField, Range(0, 100)] private float _spawnChancePercent;

    public bool EffectIsPlayed { get; private set; }
    public Biome Biome { get; private set; }

    private bool _isSuccessToSpawn;

    public event Action EffectStateChanged;

    private void OnEnable()
    {
        _isSuccessToSpawn = Random.Range(0, 101) < _spawnChancePercent;

        if(_isSuccessToSpawn)
        {
            Biome = GetComponent<Biome>();

            Biome.PlayerEntered += OnPlayerEntered;
            Biome.PlayerExited += OnPlayerExited;
        }
    }

    private void OnDisable()
    {
        if (_isSuccessToSpawn)
        {
            Biome.PlayerEntered -= OnPlayerEntered;
            Biome.PlayerExited -= OnPlayerExited;
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
