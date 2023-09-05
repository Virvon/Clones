using UnityEngine;

public class Freezbar : Bar
{
    [SerializeField] private Freezing _freezing;

    private void OnEnable() => _freezing.FreezingPercentChanged += OnFreezPrecentChanged;

    private void OnDisable() => _freezing.FreezingPercentChanged -= OnFreezPrecentChanged;

    private void Start()
    {
        Slider.value = _freezing.FreezingPercent;
        Slider.gameObject.SetActive(false);
    }

    private void OnFreezPrecentChanged()
    {
        if (_freezing.FreezingPercent <= 0)
            Slider.gameObject.SetActive(false);
        else if (_freezing.FreezingPercent > 0 && Slider.IsActive() == false)
            Slider.gameObject.SetActive(true);

        StartAnimation(_freezing.FreezingPercent);
    }
}
