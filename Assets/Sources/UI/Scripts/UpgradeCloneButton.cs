using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeCloneButton : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameObject _lockButton;
    [SerializeField] private GameObject _unlockButton;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private bool _isUseDNA;
    [SerializeField] private bool _isUseCoins;

    private CardClone _selectedCard;

    public void GetClone(CardClone selectedCard)
    {
        _selectedCard = selectedCard;
        UpdateStatus();
    }

    public void UpgradeClone()
    {
        if (_lockButton.activeInHierarchy == true)
            return;

        if (_isUseDNA)
            _selectedCard.UpgradeClone.UpgrageHealth();

        if (_isUseCoins)
            _selectedCard.UpgradeClone.UpgrageWand();
            
        UpdateStatus();
    }

    public void UpdateStatus()
    {
        _price.text = NumberFormatter.FormatNumberWithCommas(_selectedCard.UpgradeClone.PriceUpgrade);

        if (_isUseDNA)
            SetLock(_selectedCard.UpgradeClone.PriceUpgrade >= _wallet.DNA);

        if (_isUseCoins)
            SetLock(_selectedCard.UpgradeClone.PriceUpgrade >= _wallet.Coins);
    }

    private void SetLock(bool isLocked)
    {
        if (_selectedCard.IsPurchased == false)
            isLocked = true;

        _lockButton.SetActive(isLocked);
        _unlockButton.SetActive(!isLocked);
    }
}
