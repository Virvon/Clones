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

    public UnityEvent Purchased;

    private void Start()
    {
        _textPrice.text = _buyPrice.ToString();
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
            Purchased?.Invoke();
            //Destroy(this); //?
        }
    }

    private bool IsHaveCashForBuy()
    {
        return _wallet.Coins >= _buyPrice;
    }
}
