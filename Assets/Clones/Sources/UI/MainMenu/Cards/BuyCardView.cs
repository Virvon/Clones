using System;
using Clones.Auxiliary;
using Clones.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Clones.UI
{
    public class BuyCardView : MonoBehaviour, IBuyable
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private GameObject _cantBuyVisuals;
        [SerializeField] private TMP_Text _textPrice;

        private int _price;
        private Wallet _wallet;

        public event Action<Card> BuyCardTried;
        public event Action BuyTried;

        public bool CanBuy => _wallet.Money >= _price;

        private void OnDestroy()
        {
            if (_wallet != null)
                _wallet.CurrencyCountChanged -= CheckPrice;
        }

        public void Init(int price, Wallet wallet)
        {
            _price = price;
            _wallet = wallet;

            _wallet.CurrencyCountChanged += CheckPrice;

            _buyButton.onClick.AddListener(TryBuy);

            _textPrice.text = NumberFormatter.DivideIntegerOnDigits(_price);

            CheckPrice();
        }

        private void CheckPrice()
        {
            if (CanBuy)
                _cantBuyVisuals.SetActive(false);
            else
                _cantBuyVisuals.SetActive(true);
        }

        private void TryBuy()
        {
            BuyTried?.Invoke();
            BuyCardTried?.Invoke(GetComponent<Card>());
        }
    }
}