using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Clones.Data;

[RequireComponent(typeof(Button))]
public abstract class UpgradeButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _textPrice;
    [SerializeField] private GameObject _cantUpgradeVisuals;

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
}