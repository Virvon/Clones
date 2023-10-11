using UnityEngine;

public class HitColor : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _healthbleBehaviour;
    [SerializeField] private MaterialColor _materialColor;

    [SerializeField] private Color _color;
    [SerializeField] private Color _emission;
    [SerializeField] private float _delay;

    private IHealthble _healthble;

    private void OnValidate()
    {
        if(_healthbleBehaviour && _healthbleBehaviour is not IHealthble)
        {
            Debug.LogError(nameof(_healthbleBehaviour) + " needs to implement " + nameof(IHealthble));
            _healthbleBehaviour = null;
        }
    }

    private void OnEnable()
    {
        _healthble = (IHealthble)_healthbleBehaviour;

        _healthble.HealthChanged += OnDamageTaked;
    }

    private void OnDisable() => _healthble.HealthChanged -= OnDamageTaked;

    private void OnDamageTaked() => _materialColor.SetPulseColor(_color, _emission, _delay);
}
