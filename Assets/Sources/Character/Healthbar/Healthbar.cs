using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Healthbar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _healthValue;
    [SerializeField] private float _animationSpeed;
    protected abstract IHealthble Healthble { get; }

    private int _health;
    private Coroutine _animation;

    protected virtual void Start()
    {
        _health = Healthble.Health;
        _slider = GetComponentInChildren<Slider>();
        _healthValue = GetComponentInChildren<TMP_Text>();

        Healthble.DamageTaked += OnDamageTaked;

        Init();
    }

    private void OnDisable() => Healthble.DamageTaked -= OnDamageTaked;

    private void Init()
    {
        _slider.value = Mathf.Clamp(Healthble.Health / _health, 0, 1);
        _healthValue.text = _health.ToString();
    }

    private void OnDamageTaked()
    {
        if (_animation != null)
            StopCoroutine(_animation);

        float targetValue = Mathf.Clamp((float)Healthble.Health / _health, 0, 1);

        _animation = StartCoroutine(Animation(targetValue));
        _healthValue.text = Healthble.Health.ToString();
    }

    private IEnumerator Animation(float targetValue)
    {
        float time = 0;
        float startValue = _slider.value;

        while(_slider.value != targetValue)
        {
            time += _animationSpeed / Time.deltaTime;
            _slider.value = Mathf.Lerp(startValue, targetValue, time);
            
            yield return null;
        }
    }
}
