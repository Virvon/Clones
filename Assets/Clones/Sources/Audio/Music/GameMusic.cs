using Clones.GameLogic;
using Clones.Types;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.Audio
{
    public class GameMusic : MonoBehaviour
    {
        [SerializeField] private AudioSource _idleTheame;
        [SerializeField] private AudioSource _defaultKombatTheme;
        [SerializeField] private AudioSource _iceCrystalsKombatTheme;
        [SerializeField] private AudioSource _poisonForestKombatTheme;
        [SerializeField] private AudioSource _viscoseSalviaKombatTheme;

        private Dictionary<BiomeType, AudioSource> _audioSources;

        private EnemiesSpawner _enemiesSpawner;
        private ICurrentBiome _currentBiome;

        public void Init(EnemiesSpawner enemiesSpawner, ICurrentBiome currentBiome)
        {
            _enemiesSpawner = enemiesSpawner;
            _currentBiome = currentBiome;

            _audioSources = new();

            _audioSources.Add(BiomeType.IceCrystals, _iceCrystalsKombatTheme);
            _audioSources.Add(BiomeType.ViscousSalvia, _viscoseSalviaKombatTheme);
            _audioSources.Add(BiomeType.PoisonForest, _poisonForestKombatTheme);
            _audioSources.Add(BiomeType.Wasterlend, _defaultKombatTheme);
            _audioSources.Add(BiomeType.Forest, _defaultKombatTheme);

            _enemiesSpawner.CreatedWave += OnCreatedWave;
        }

        private void OnDisable() => 
            _enemiesSpawner.CreatedWave -= OnCreatedWave;

        private void OnCreatedWave()
        {
            foreach(var audio in _audioSources.Values)
                audio.Stop();

            _audioSources[_currentBiome.Type].Play();
        }
    }
}
