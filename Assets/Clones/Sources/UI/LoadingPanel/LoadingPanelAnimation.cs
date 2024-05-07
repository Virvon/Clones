using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class LoadingPanelAnimation : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField, Range(0, 1)] private float _maxTransparency;
        [SerializeField, Range(0, 1)] private float _minTransparency;
        [SerializeField] private float _speed;

        private bool _isWorked;


        public void OnEnable() => 
            StartCoroutine(Animator());

        private void OnDisable() => 
            _isWorked = false;

        private void LerpTransparency(float startTransparency, float targetTransparency, float t) => 
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, Mathf.Lerp(startTransparency, targetTransparency, t));

        private IEnumerator Animator()
        {
            bool isTransparencyIncreases = false;
            float time = 0;

            _isWorked = true;

            while (_isWorked)
            {
                if(isTransparencyIncreases)
                    LerpTransparency(_minTransparency, _maxTransparency, time);
                else
                    LerpTransparency(_maxTransparency, _minTransparency, time);

                time += Time.deltaTime * _speed;

                if(time > 1)
                {
                    time = 0;
                    isTransparencyIncreases = !isTransparencyIncreases;
                }

                yield return null;
            }
        }
    }
}