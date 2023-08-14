using Clones.Biomes;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Biome))]
public class Fog : MonoBehaviour
{
    [SerializeField] private float _density;
    [SerializeField] private float _foggingSpeed;
    [SerializeField] private Color _color;

    private Biome _biome;
    private Coroutine _fogDensity;

    private void OnEnable()
    {
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
        RenderSettings.fogColor = _color;

        SetFogDensity(_density, _foggingSpeed);
    }

    private void OnPlayerExited() => SetFogDensity(0, _foggingSpeed);

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

            RenderSettings.fogDensity = Mathf.Lerp(startDensity, targetDensity, time / foggingSpeed);

            yield return null;
        }
    }
}
