using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Bar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _animationSpeed;

    protected Slider Slider => _slider;

    private Coroutine _animation;

    protected void StartAnimation(float targetValue)
    {
        if (_animation != null)
            StopCoroutine(_animation);

        _animation = StartCoroutine(Animation(targetValue));
    }

    private IEnumerator Animation(float targetValue)
    {
        float time = 0;
        float startValue = _slider.value;

        while (_slider.value != targetValue)
        {
            time += Time.deltaTime;
            _slider.value = (float)Math.Round(Mathf.Lerp(startValue, targetValue, time / _animationSpeed), 3);

            yield return null;
        }
    }
}
