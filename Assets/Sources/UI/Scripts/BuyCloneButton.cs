using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyCloneButton : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameObject _lockButton;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private CardClone _cardClone;

    private void Start()
    {
        UpdateStatus();
        _price.text = NumberFormatter.FormatNumberWithCommas(_cardClone.Price);
    }

    public void BuyClone()
    {
        if (_lockButton.activeInHierarchy == true)
            return;

        _wallet.ChangeCoinsCount(-_cardClone.Price);
        _cardClone.SwitchWisiblePanels(true, false, false);
        _cardClone.Buy();
    }

    public void UpdateStatus()
    {
        _lockButton.SetActive(_cardClone.Price > _wallet.Coins);
    }
}
