using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Clones.Data;

namespace Clones.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class UpgradeButton : MonoBehaviour, IBuyable
    {
        [SerializeField] private TMP_Text _textPrice;
        [SerializeField] private GameObject _cantUpgradeVisuals;

        private Button _button;

        public abstract bool CanBuy { get; }

        protected int Price { get; private set; }
        protected Wallet Wallet { get; private set; }

        public event Action BuyTried;

        private void OnDisable()
        {
            Wallet.CurrencyCountChanged -= CheckPrice;
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        public void Init(Wallet wallet)
        {
            Wallet = wallet;

            _button = GetComponent<Button>();

            Wallet.CurrencyCountChanged += CheckPrice;
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

        private void OnButtonClicked() =>
            BuyTried?.Invoke();

        private void CheckPrice()
        {
            if (CanBuy)
                _cantUpgradeVisuals.SetActive(false);
            else
                _cantUpgradeVisuals.SetActive(true);
        }
    }
}