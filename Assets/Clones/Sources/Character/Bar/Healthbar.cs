using TMPro;
using UnityEngine;

public abstract class Healthbar : Bar
{
    [SerializeField] private TMP_Text _healthValue;
    private IHealthble _healthble;

    private int _health;

    private void OnDisable() => _healthble.HealthChanged -= OnDamageTaked;

    protected void TakeHealthble(IHealthble healthble)
    {
        _healthble = healthble;
        _health = _healthble.Health;
        _healthValue.text = _health.ToString();

        Slider.value = Mathf.Clamp(_healthble.Health / _health, 0, 1);

        _healthble.HealthChanged += OnDamageTaked;
    }

    private void OnDamageTaked()
    {
        float targetValue = Mathf.Clamp((float)_healthble.Health / _health, 0, 1);
        _healthValue.text = _healthble.Health.ToString();

        StartAnimation(targetValue);
    }
}
