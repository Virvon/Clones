using Clones.Auxiliary;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class DieClone : MonoBehaviour
    {
        [SerializeField] private GameObject _dieVisuals;
        [SerializeField] private TMP_Text _timeToRestoreText;

        private const float Delay = 1;

        private DateTime _disuseEndDate;

        public bool IsUsed { get; private set; }

        public event Action Disused;

        public void Init(DateTime disuseEndDate)
        {
            _disuseEndDate = disuseEndDate;

            Wait();
        }

        private void OnEnable() =>
            Wait();

        private void Wait()
        {
            TimeSpan timeLeft = _disuseEndDate - DateTime.Now;

            if (timeLeft > TimeSpan.Zero)
                Use();
            else
                EndUse();
        }

        private void EndUse()
        {
            IsUsed = false;
            _dieVisuals.SetActive(false);

            Disused?.Invoke();
        }

        private void Use()
        {
            IsUsed = true;
            _dieVisuals.SetActive(true);

            StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            var delay = new WaitForSeconds(Delay);
            bool isTimeUp = false;
            TimeSpan timeLeft;

            while (isTimeUp == false)
            {
                timeLeft = _disuseEndDate - DateTime.Now;

                if (timeLeft > TimeSpan.Zero)
                    _timeToRestoreText.text = NumberFormatter.ConvertSecondsToTimeString((float)timeLeft.TotalSeconds);
                else
                    isTimeUp = true;

                yield return delay;
            }

            EndUse();
        }
    }
}