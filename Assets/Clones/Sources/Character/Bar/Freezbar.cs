public class Freezbar : Bar
{
    private IFreezingPercentChanger _freezingPercentChanger;

    private void OnDisable() => _freezingPercentChanger.FreezingPercentChanged -= OnFreezPrecentChanged;

    private void Start()
    {
        Slider.value = _freezingPercentChanger.FreezingPercent;
        Slider.gameObject.SetActive(false);
    }

    public void Init(IFreezingPercentChanger freezingPercentChanger)
    {
        _freezingPercentChanger = freezingPercentChanger;
        _freezingPercentChanger.FreezingPercentChanged += OnFreezPrecentChanged;
    }

    private void OnFreezPrecentChanged()
    {
        if (_freezingPercentChanger.FreezingPercent <= 0)
            Slider.gameObject.SetActive(false);
        else if (_freezingPercentChanger.FreezingPercent > 0 && Slider.IsActive() == false)
            Slider.gameObject.SetActive(true);

        StartAnimation(_freezingPercentChanger.FreezingPercent);
    }
}
