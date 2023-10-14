using Clones.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyCardView : MonoBehaviour
{
    [SerializeField] private Button _selectButton;
    [SerializeField] private Button _buyButton;
    [SerializeField] private GameObject _cantBuyVisuals;
    [SerializeField] private TMP_Text _textPrice;
    [Space]
    [SerializeField] private GameObject _lockVisuals;
    [SerializeField] private GameObject _unlockVisuals;

    private int _price;
    private Wallet _wallet;

    private bool _canBuy => _wallet.Money >= _price;

    public void Init(int price, Wallet wallet)
    {
        _price = price;
        _wallet = wallet;

        _wallet.CurrencyCountChanged += CheckPrice;
        _buyButton.onClick.AddListener(TryBuy);

        _selectButton.interactable = false;
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

    private void TryBuy()
    {
        if (_canBuy)
        {
            _selectButton.interactable = true;
            _lockVisuals.SetActive(false);
            _unlockVisuals.SetActive(true);
        }
    }
}
