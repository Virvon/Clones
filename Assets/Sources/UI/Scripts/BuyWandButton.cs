using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyWandButton : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private GameObject _lockButton;
    [SerializeField] private TMP_Text _price;
    [SerializeField] private CardWand _cardWand;

    private void Start()
    {
        UpdateStatus();
        _price.text = NumberFormatter.FormatNumberWithCommas(_cardWand.Price);
    }

    public void BuyWand()
    {
        if (_lockButton.activeInHierarchy == true)
            return;

        _wallet.ChangeCoinsCount(-_cardWand.Price);
        _cardWand.SwitchWisiblePanels(true, false);
        _cardWand.Buy();
    }

    public void UpdateStatus()
    {
        _lockButton.SetActive(_cardWand.Price > _wallet.Coins);
    }
}
