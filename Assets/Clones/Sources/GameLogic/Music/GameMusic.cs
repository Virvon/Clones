using Clones.Types;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.GameLogic
{
    public class GameMusic : MonoBehaviour
    {
        private Dictionary<BiomeType, AudioSource> _combatMusic = new();
        private AudioSource _idleMusic;
        private ICurrentBiome _currentBiome;
        private EnemiesSpawner _enemiesSpawner;
        private AudioSource _currentAudioSource;

        public void Init(AudioSource idleMusicAudioSourcePrefab, ICurrentBiome currentBiome, EnemiesSpawner enemiesSpawner)
        {
            _idleMusic = Instantiate(idleMusicAudioSourcePrefab, transform);
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
