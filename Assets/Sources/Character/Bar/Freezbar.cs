using UnityEngine;

public class Freezbar : Bar
{
    [SerializeField] private Freezing _freezing;

    private void OnEnable() => _freezing.FreePrecentChanged += OnFreezPrecentChanged;

    private void OnDisable() => _freezing.FreePrecentChanged -= OnFreezPrecentChanged;

    private void Start()
    {
        Slider.value = _freezing.FreezPrecent;
        Slider.gameObject.SetActive(false);
    }

    private void OnFreezPrecentChanged()
    {
        if (_freezing.FreezPrecent <= 0)
            Slider.gameObject.SetActive(false);
        else if (_freezing.FreezPrecent > 0 && Slider.IsActive() == false)
            Slider.gameObject.SetActive(true);

        StartAnimation(_freezing.FreezPrecent);
    }
}
