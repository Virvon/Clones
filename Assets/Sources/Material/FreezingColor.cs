using UnityEngine;

public class FreezingColor : MonoBehaviour
{
    [SerializeField] private MaterialColor _materialColor;
    [SerializeField] private Freezing _freezing;

    [SerializeField] private Color _color;
    [SerializeField] private Color _emission;

    private void OnEnable() => _freezing.FreezingPercentChanged += OnFreezingPercentChanged;

    private void OnDisable() => _freezing.FreezingPercentChanged -= OnFreezingPercentChanged;

    private void OnFreezingPercentChanged()
    {
        _materialColor.LerpColor(_materialColor.StartColor, _color, _freezing.FreezingPercent);
        _materialColor.LerpEmission(_materialColor.StartEmission, _emission, _freezing.FreezingPercent);
    }
}
