using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class MaterialColor : MonoBehaviour
{
    public Color StartColor { get; private set; }
    public Color StartEmission { get; private set; }
    public Color CurrentColor { get; private set; }
    public Color CurrentEmission { get; private set; }

    private Material _material;
    private Coroutine _pulsator;
    private bool _isColorPulsed;

    private void Awake()
    {
        _material = GetComponent<SkinnedMeshRenderer>().material;

        StartColor = _material.color;
        StartEmission = _material.GetColor("_EmissionColor");
        CurrentColor = StartColor;
        CurrentEmission = StartEmission;
    }

    public void LerpColor(Color start, Color end, float interpolation)
    {
        CurrentColor = Color.Lerp(start, end, interpolation);

        if (_isColorPulsed == false)
            _material.color = CurrentColor;
    }

    public void LerpEmission(Color start, Color end, float interpolation)
    {
        CurrentEmission = Color.Lerp(start, end, interpolation);

        if (_isColorPulsed == false)
            _material.SetColor("_EmissionColor", CurrentEmission);
    }

    public void SetPulseColor(Color color, Color emission, float delay)
    {
        if (_pulsator != null)
            return;

        _pulsator = StartCoroutine(Pulsator(color, emission, delay));
    }

    private IEnumerator Pulsator(Color targetColor, Color targetEmission, float delay)
    {
        _isColorPulsed = true;

        float time = 0;

        while (time < delay / 2)
        {
            time += Time.deltaTime;

            _material.color = Color.Lerp(CurrentColor, targetColor, time / (delay / 2));
            _material.SetColor("_EmissionColor", Color.Lerp(CurrentEmission, targetEmission, time / (delay / 2)));

            yield return null;
        }

        while (time < delay)
        {
            time += Time.deltaTime;

            _material.color = Color.Lerp(targetColor, CurrentColor, time / delay);
            _material.SetColor("_EmissionColor", Color.Lerp(targetEmission, CurrentEmission, time / delay));

            yield return null;
        }

        _isColorPulsed = false;

        _pulsator = null;
    }
}
