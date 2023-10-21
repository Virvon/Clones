using Clones.Data;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyCardView : MonoBehaviour
{
    [SerializeField] private Button _buyButton;
    [SerializeField] private GameObject _cantBuyVisuals;
    [SerializeField] private TMP_Text _textPrice;

    private int _price;
    private Wallet _wallet;

    public event Action<Card> BuyTried;

    private bool _canBuy => _wallet.Money >= _price;

    private void OnDestroy()
    {
        if(_wallet != null)
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
        if (_canBuy)
            _cantBuyVisuals.SetActive(false);
        else
            _cantBuyVisuals.SetActive(true);
    }

    private void TryBuy() =>
        BuyTried?.Invoke(GetComponent<Card>());
}