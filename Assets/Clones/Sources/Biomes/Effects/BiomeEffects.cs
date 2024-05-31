using System;
using Clones.Biomes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Clones.BiomeEffects
{
    public class BiomeEffects : MonoBehaviour
    {
        [SerializeField] private Biome _biome;
        [SerializeField][Range(0, 100)] private float _spawnChancePercent;

        private bool _isSuccessToSpawn;

        public event Action EffectStateChanged;

        public bool EffectIsPlayed { get; private set; }
        public Biome Biome => _biome;

        private void OnEnable()
        {
            _isSuccessToSpawn = Random.Range(0, 101) < _spawnChancePercent;

            if (_isSuccessToSpawn)
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
}