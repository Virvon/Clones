using Clones.Types;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.GameLogic
{
    public class GameMusic : MonoBehaviour
    {
        [SerializeField] private AudioSource _idleMusic;

        private Dictionary<BiomeType, AudioSource> _combatMusic;
        private ICurrentBiome _currentBiome;
        private EnemiesSpawner _enemiesSpawner;
        private AudioSource _currentAudioSource;

        public void Init(ICurrentBiome currentBiome, EnemiesSpawner enemiesSpawner)
        {
            _currentBiome = currentBiome;
            _enemiesSpawner = enemiesSpawner;

            _combatMusic = new();

            PlayIdleMusic();

            _enemiesSpawner.CreatedWave += OnCreatedWave;
        }

        public void Add(BiomeType biomeType, AudioSource audioSourcePrefab)
        {
            AudioSource existingAudioSource = _combatMusic.Values.Where(value => value.clip == audioSourcePrefab.clip).FirstOrDefault();

            if (existingAudioSource != null)
                _combatMusic.Add(biomeType, existingAudioSource);
            else
                _combatMusic.Add(biomeType, Instantiate(audioSourcePrefab, transform));
        }

        private void PlayIdleMusic()
        {
            _currentAudioSource?.Stop();
            _currentAudioSource = _idleMusic;
            _currentAudioSource.Play();
        }

        private void OnCreatedWave()
        {
            if (_currentAudioSource != _idleMusic)
                return;

            _currentAudioSource?.Pause();
            _currentAudioSource = _combatMusic[_currentBiome.Type];
            _currentAudioSource.Play();

            StartCoroutine(IdleWaiter());
        }

        private bool IsWaveNotEnded() =>
            _enemiesSpawner.GetEnemiesCount() > 0;

        private IEnumerator IdleWaiter()
        {
            yield return new WaitWhile(IsWaveNotEnded);

            PlayIdleMusic();
        }
    }
}