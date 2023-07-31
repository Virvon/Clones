using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameObject _lockButton;
    [SerializeField] private GameObject _unlockButton;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private bool _isUseDNA;
    [SerializeField] private bool _isUseCoins;
    [SerializeField] private CardClone _cardClone;

    private CardClone _selectedCard;

    private void Start()
    {
        if(_cardClone != null)
        {
            _lockButton.SetActive(_cardClone.Price > _wallet.Coins);
            _unlockButton.SetActive(_cardClone.Price <= _wallet.Coins);
        }
    }

    public void GetClone(CardClone card)
    {
        _selectedCard = card;
        UpdateStatus();
    }

    public void SetAvailabilityBuy()
    {
        SetLock(_cardClone.Price > _wallet.Coins);
    }

    public void BuyClone()
    {
        if (_lockButton.active == true)
            return;

        _wallet.ChangeCoinsCount(-_cardClone.Price);
        _cardClone.SwitchWisiblePanels(true, false, false);
    }

    public void UpgradeClone()
    {
        if (_lockButton.active == true)
            return;

        if (_isUseDNA)
            _selectedCard.UpgradeClone.UpgrageHealth();

        if (_isUseCoins)
            _selectedCard.UpgradeClone.UpgrageWand();
            
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        _price.text = NumberFormatter.FormatNumberWithCommas(_selectedCard.UpgradeClone.PriceUpgrade);
        _price.text = NumberFormatter.FormatNumberWithCommas(_selectedCard.UpgradeClone.PriceUpgrade);

        if (!_selectedCard.IsPurchased)
        {
            SetLock(true);
            return;
        }

        if (_isUseDNA)
            SetLock(_selectedCard.UpgradeClone.PriceUpgrade >= _wallet.DNA);

        if (_isUseCoins)
            SetLock(_selectedCard.UpgradeClone.PriceUpgrade >= _wallet.Coins);
    }

    private void SetLock(bool isLocked)
    {
        _lockButton.SetActive(isLocked);
        _unlockButton.SetActive(!isLocked);
    }
}
