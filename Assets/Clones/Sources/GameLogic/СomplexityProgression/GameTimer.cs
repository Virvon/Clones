using Clones.Services;
using System.Collections;
using UnityEngine;

namespace Clones.GameLogic
{
    public class GameTimer : ITimeScalable
    {
        private bool _isStarted;
        private ICoroutineRunner _coroutineRunner;
        private float _currentMeasurement;
        private float _timeScale;

        public float LastMeasurement { get; private set; }

        public void Init(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;

            _isStarted = false;
            _currentMeasurement = 0;
            _timeScale = 1;
        }

        public void Start()
        {
            if (_isStarted)
                return;

            _isStarted = true;
            _coroutineRunner.StartCoroutine(Timer());
        }

        public float Stop()
        {
            if (_isStarted == false)
                return 0;

            _isStarted = false;
            LastMeasurement = _currentMeasurement;

            return LastMeasurement;
        }

        public void ScaleTime(float scale) => 
            _timeScale = scale;

        private IEnumerator Timer()
        {
            while (_isStarted)
            {
                _currentMeasurement += Time.deltaTime * _timeScale;
                yield return null;
            }
        }
    }
}