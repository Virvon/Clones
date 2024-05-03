using Clones.GameLogic;
using Clones.Infrastructure;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class GameRevivalView : MonoBehaviour, IOpenableView
    {
        [SerializeField] private GameObject _background;
        [SerializeField] private TMP_Text _timeValue;
        [SerializeField] private float _cooldown;
        [SerializeField] private RevivalViewAnimator _revivalViewAnimator;

        private IPlayerRevival _playerRevival;

        private GameOverView _gameOverView;
        private Coroutine _timer;

        public Action Opened;

        public void Init(IPlayerRevival playerRevival, GameOverView gameOverView)
        {
            _playerRevival = playerRevival;
            _gameOverView = gameOverView;
            _timeValue.text = _cooldown.ToString();
        }

        public void Open()
        {
            Opened?.Invoke();

            if (_playerRevival.CanRivival)
            {
                _background.SetActive(true);

                if (_timer != null)
                    StopCoroutine(_timer);

                _revivalViewAnimator.Open(() => _timer = StartCoroutine(Timer()));
            }
            else
            {
                _gameOverView.Open();
            }
        }

        public void Close(Action callback = null) => 
            _revivalViewAnimator.Close(callback);

        public void StopTimer() => 
            StopCoroutine(_timer);

        private IEnumerator Timer()
        {
            float currentCooldown = _cooldown;

            _timeValue.text = currentCooldown.ToString();

            while (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;

                _timeValue.text = Mathf.Round(currentCooldown).ToString();

                yield return null;
            }

            _revivalViewAnimator.Close(()=> _gameOverView.Open());
        }
    }
}