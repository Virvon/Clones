﻿using Clones.GameLogic;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class RevivalView : MonoBehaviour
    {
        [SerializeField] private GameObject _background;
        [SerializeField] private TMP_Text _timeValue;
        [SerializeField] private float _cooldown;

        private PlayerRevival _playerRevival;
        private GameOverView _gameOverView;
        private Coroutine _timer;

        public void Init(PlayerRevival playerRevival, GameOverView gameOverView)
        {
            _playerRevival = playerRevival;
            _gameOverView = gameOverView;
        }

        public void Open()
        {
            if (_playerRevival.CanRivival)
            {
                _background.SetActive(true);

                if (_timer != null)
                    StopCoroutine(_timer);

                _timer = StartCoroutine(Timer());
            }
            else
            {
                _gameOverView.Open();
            }
        }

        public void Close()
        {
            _background.SetActive(false);
        }

        public void StopTimer()
        {
            StopCoroutine(_timer);
        }

        private IEnumerator Timer()
        {
            float currentCooldown = _cooldown;

            _timeValue.text = currentCooldown.ToString();

            while (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;

                _timeValue.text = Mathf.Round(currentCooldown).ToString();

                yield return new WaitForFixedUpdate();
            }

            Close();
            _gameOverView.Open();
        }
    }
}