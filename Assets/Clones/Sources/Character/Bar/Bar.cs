using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Clones.Character.Bars
{
    public abstract class Bar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _animationSpeed;

        private Coroutine _animation;

        protected Slider Slider => _slider;

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

            targetValue = (float)Math.Round(targetValue, 3);

            while (_slider.value != targetValue)
            {
                time += Time.deltaTime;
                _slider.value = (float)Math.Round(Mathf.Lerp(startValue, targetValue, time / _animationSpeed), 3);

                yield return null;
            }
        }
    }
}
