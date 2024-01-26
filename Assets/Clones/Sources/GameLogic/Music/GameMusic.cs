using Clones.Types;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.GameLogic
{
    public class GameMusic : MonoBehaviour
    {
        [SerializeField] private AudioSource _idleMusic;

        private Dictionary<BiomeType, AudioSource> _combatMusic = new();
        private ICurrentBiome _currentBiome;
        private EnemiesSpawner _enemiesSpawner;
        private AudioSource _currentAudioSource;

        public void Init(ICurrentBiome currentBiome, EnemiesSpawner enemiesSpawner)
        {
            _currentBiome = currentBiome;
            _enemiesSpawner = enemiesSpawner;

            _currentAudioSource = _idleMusic;
        }

        public void Add(BiomeType biomeType, AudioSource audioSourcePrefab)
        {
            AudioSource existingAudioSource = _combatMusic.Values.Where(value => value == audioSourcePrefab).FirstOrDefault();

            if (existingAudioSource != null)
                _combatMusic.Add(biomeType, existingAudioSource);
            else
                _combatMusic.Add(biomeType, Instantiate(audioSourcePrefab, transform));
        }
    }
}
