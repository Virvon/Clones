using Clones.Data;
using System;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public abstract class CurrencyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currencyValue;

        protected Wallet Wallet { get; private set; }
        protected TMP_Text CurrencyValue => _currencyValue;

        public void Init(Wallet wallet)
        {
            Wallet = wallet;

            Wallet.CurrencyCountChanged += UpdateCurrencyValue;
        }

        private void Start() =>
            UpdateCurrencyValue();

        private void OnDisable() =>
            Wallet.CurrencyCountChanged -= UpdateCurrencyValue;

        protected abstract void UpdateCurrencyValue();
    }
}
