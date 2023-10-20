using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Clones.Data;

[RequireComponent(typeof(Button))]
public abstract class UpgradeButton : MonoBehaviour
{
    //[SerializeField] private Wallet _wallet;
    [Space]
    [SerializeField] private TMP_Text _textPrice;
    [Space]
    [SerializeField] private GameObject _cantUpgradeVisuals;
    //[Space]
    //[SerializeField] private bool _isUseDNA;
    //[SerializeField] private bool _isUseCoins;

    private Button _button;

    protected int Price { get; private set; }
    protected Wallet Wallet { get; private set; }

    protected abstract bool CanUpgrade { get; }

    public event Action UpgradeTried;

    private void OnDisable() => 
        _button.onClick.RemoveListener(OnButtonClicked);

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

    private void OnButtonClicked() => 
        UpgradeTried?.Invoke();

    private void CheckPrice()
    {
        if (CanUpgrade)
            _cantUpgradeVisuals.SetActive(false);
        else
            _cantUpgradeVisuals.SetActive(true);
    }

    public void OnClick()
    {
        //Debug.Log("upgrade button click");
        //if (_isUseDNA)
        //{
        //    //_wallet.ChangeDNACount(-_cardClone.UpgradePrice);
        //    //_cardClone.UpgradeByDNA();
        //}

        //if (_isUseCoins)
        //{
        //    //_wallet.ChangeCoinsCount(-_cardClone.UpgradePrice);
        //    //_cardClone.UpgradeByCoins();
        //}
    }

    public void SetCardClone(CloneCard cardClone)
    {
        //_cardClone = cardClone;
    }

    public void UpdateButton()
    {
        Debug.Log("upgrade button");
        //int price = _cardClone.UpgradePrice;

        //if (_isUseDNA)
        //{
        //    //_cantUpgradeVisuals.SetActive(_wallet.DNA < price);
        //    //_button.interactable = _wallet.DNA >= price;
        //}

        //if (_isUseCoins)
        //{
        //    //_cantUpgradeVisuals.SetActive(_wallet.Coins < price);
        //    //_button.interactable = _wallet.Coins >= price;
        //}
        
        //_textPrice.text = NumberFormatter.DivideIntegerOnDigits(price);
    }
}