using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BuyCard : MonoBehaviour
{
    [SerializeField] private int _buyPrice;
    [SerializeField] private GameObject _buyButton;
    [SerializeField] private GameObject _cantBuyVisuals;
    [SerializeField] private TMP_Text _textPrice;
    [SerializeField] private Wallet _wallet;
    [Space]
    [SerializeField] private GameObject _lockVisuals;
    [SerializeField] private GameObject _unlockVisuals;

    public UnityEvent Purchased = new UnityEvent();

    private void Start()
    {
        _wallet.ValuesChanged.AddListener(CheckCanBuy);
        _textPrice.text = NumberFormatter.FormatNumberWithCommas(_buyPrice);
        CheckCanBuy();
    }

    public void CheckCanBuy()
    {
        _cantBuyVisuals.SetActive(IsHaveCashForBuy() == false);
    }

    public void Invoke()
    {
        if (IsHaveCashForBuy())
        {
            _buyButton.SetActive(false);
            _lockVisuals.SetActive(false);
            _unlockVisuals.SetActive(true);
            _wallet.ChangeCoinsCount(-_buyPrice);
            Purchased?.Invoke();
            Destroy(this);
        }
    }

    private bool IsHaveCashForBuy()
    {
        return _wallet.Coins >= _buyPrice;
    }
}
