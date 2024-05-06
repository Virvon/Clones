using System.Collections;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BiomeEffects))]
public class Precipitation : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _precipitationsPrefabs;
    [SerializeField] private float _destroyTime;

    private BiomeEffects _biomeEffects;
    private ParticleSystem _currentPrecipitation;
    private ParticleSystem _precipitationPrefab;
    private Coroutine _precipitationDestroyer;

    private void OnEnable()
    {
        if (_precipitationsPrefabs == null)
            throw new NullReferenceException(nameof(_precipitationsPrefabs));

        _precipitationPrefab = _precipitationsPrefabs[Random.Range(0, _precipitationsPrefabs.Length)];
        _biomeEffects = GetComponent<BiomeEffects>();

        _biomeEffects.EffectStateChanged += OnEffectStateChanged;
    }

    private void OnDisable() => 
        _biomeEffects.EffectStateChanged -= OnEffectStateChanged;

    private void OnEffectStateChanged()
    {
        if (_biomeEffects.EffectIsPlayed)
            PlayEffect();
        else
            StopEffect();
    }

    private void PlayEffect()
    {
        if (_currentPrecipitation != null)
        {
            if (_precipitationDestroyer != null)
                StopCoroutine(_precipitationDestroyer);

            _currentPrecipitation.Play();
        }
        else
        {
            _currentPrecipitation = Instantiate(_precipitationPrefab, _biomeEffects.Biome.Player.transform.position, _biomeEffects.Biome.Player.transform.rotation, _biomeEffects.Biome.Player.transform);
        }
    }

    private void StopEffect()
    {
        if (_precipitationDestroyer != null)
            StopCoroutine(_precipitationDestroyer);

        _precipitationDestroyer = StartCoroutine(PrecipitationDestroyer());
    }

    private IEnumerator PrecipitationDestroyer()
    {
        _currentPrecipitation.Stop();

        yield return new WaitForSeconds(_destroyTime);

        Destroy(_currentPrecipitation.gameObject);
    }
}
