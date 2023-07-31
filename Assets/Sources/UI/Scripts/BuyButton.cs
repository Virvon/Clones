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

    private CardClone _card;

    public void GetClone(CardClone card)
    {
        _card = card;
        UpdateStatus();
    }

    public void UpgradeClone()
    {
        if (_lockButton.active == true)
            return;

        if (_isUseDNA)
            _card.UpgradeClone.UpgrageHealth();

        if (_isUseCoins)
            _card.UpgradeClone.UpgrageWand();
            
        

        UpdateStatus();
    }

    private void UpdateStatus()
    {
        _price.text = NumberFormatter.FormatNumberWithCommas(_card.UpgradeClone.PriceUpgrade);
        _price.text = NumberFormatter.FormatNumberWithCommas(_card.UpgradeClone.PriceUpgrade);

        if (_isUseDNA)
            SetLock(_card.UpgradeClone.PriceUpgrade >= _wallet.DNA);

        if (_isUseCoins)
            SetLock(_card.UpgradeClone.PriceUpgrade >= _wallet.Coins);
    }

    private void SetLock(bool isLocked)
    {
        _lockButton.SetActive(isLocked);
        _unlockButton.SetActive(!isLocked);
    }
}
