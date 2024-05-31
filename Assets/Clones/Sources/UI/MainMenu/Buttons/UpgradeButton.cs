using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Clones.Data;
using Clones.Services;
using Clones.Auxiliary;

namespace Clones.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class UpgradeButton : MonoBehaviour, IBuyable, IProgressReader
    {
        [SerializeField] private TMP_Text _textPrice;
        [SerializeField] private GameObject _cantUpgradeVisuals;

        private Button _button;

        public event Action BuyTried;

        public abstract bool CanBuy { get; }

        protected int Price { get; private set; }
        protected IPersistentProgressService PersistentProgress { get; private set; }

        private void OnDisable()
        {
            Unsubscribe();
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        public void Init(IPersistentProgressService persistentProgress)
        {
            PersistentProgress = persistentProgress;

            _button = GetComponent<Button>();

            Subscribe();
            _button.onClick.AddListener(OnButtonClicked);

            CheckPrice();
        }

        public void SetPrice(int price)
        {
            Price = price;

            _textPrice.text = NumberFormatter.DivideIntegerOnDigits(price);
            CheckPrice();
        }

        public void Deactive()
        {
            _cantUpgradeVisuals.SetActive(true);
            _textPrice.text = "";
        }

        public void UpdateProgress()
        {
            Unsubscribe();
            Subscribe();
        }

        private void OnButtonClicked() =>
            BuyTried?.Invoke();

        private void CheckPrice()
        {
            if (CanBuy)
                _cantUpgradeVisuals.SetActive(false);
            else
                _cantUpgradeVisuals.SetActive(true);
        }

        private void Subscribe() => 
            PersistentProgress.Progress.Wallet.CurrencyCountChanged += CheckPrice;

        private void Unsubscribe() =>
            PersistentProgress.Progress.Wallet.CurrencyCountChanged -= CheckPrice;
    }
}