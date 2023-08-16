using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BiomeEffects))]
public class Fog : MonoBehaviour
{
    [SerializeField] private float _density;
    [SerializeField] private float _foggingSpeed;
    [SerializeField] private Color _color;

    private BiomeEffects _biomeEffects;
    private Coroutine _fogDensity;

    private void OnEnable()
    {
        _biomeEffects = GetComponent<BiomeEffects>();

        _biomeEffects.EffectStateChanged += OnEffectStateChanged;
    }

    private void OnDisable() => _biomeEffects.EffectStateChanged -= OnEffectStateChanged;

    private void OnEffectStateChanged()
    {
        if(_biomeEffects.EffectIsPlayed)
        {
            RenderSettings.fogColor = _color;

            SetFogDensity(_density, _foggingSpeed);
        }
        else
        {
            SetFogDensity(0, _foggingSpeed);
        }
    }

    private void SetFogDensity(float targetDensity, float foggingSpeed)
    {
        if (_fogDensity != null)
            StopCoroutine(_fogDensity);

        _fogDensity = StartCoroutine(FogDensity(targetDensity, foggingSpeed));
    }

    private IEnumerator FogDensity(float targetDensity, float foggingSpeed)
    {
        float time = 0;
        float startDensity = RenderSettings.fogDensity;

        while(RenderSettings.fogDensity != targetDensity)
        {
            time += Time.deltaTime;

            RenderSettings.fogDensity = (float)Math.Round(Mathf.Lerp(startDensity, targetDensity, time / foggingSpeed), 3);

            yield return null;
        }
    }
}
