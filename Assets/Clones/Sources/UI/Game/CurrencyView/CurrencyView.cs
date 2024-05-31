using Clones.Data;
using Clones.Services;
using System;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public abstract class CurrencyView : MonoBehaviour, IProgressReader
    {
        [SerializeField] private TMP_Text _currencyValue;
        protected IPersistentProgressService PersistentProgress { get; private set; }
        protected TMP_Text CurrencyValue => _currencyValue;

        private void Start() =>
            UpdateCurrencyValue();

        private void OnDisable() =>
            Unsubscribe();

        public void Init(IPersistentProgressService persistentProgress)
        {
            PersistentProgress = persistentProgress;

            Subscribe();
        }

        public void UpdateProgress()
        {
            Unsubscribe();
            Subscribe();
            UpdateCurrencyValue();
        }

        protected abstract void UpdateCurrencyValue();

        private void Subscribe() =>
            PersistentProgress.Progress.Wallet.CurrencyCountChanged += UpdateCurrencyValue;

        private void Unsubscribe() =>
            PersistentProgress.Progress.Wallet.CurrencyCountChanged -= UpdateCurrencyValue;
    }
}
