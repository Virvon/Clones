using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [Space]
    [SerializeField] private TMP_Text _textPrice;
    [Space]
    [SerializeField] private GameObject _cantUpgradeVisuals;
    [Space]
    [SerializeField] private CardClone _cardClone;
    [Space]
    [SerializeField] private bool _isUseDNA;
    [SerializeField] private bool _isUseCoins;

    public void OnClick()
    {
        if (_isUseDNA)
            _cardClone.UpgradeByDNA();

        if (_isUseCoins)
            _cardClone.UpgradeByCoins();
    }

    public void SetCardClone(CardClone cardClone)
    {
        _cardClone = cardClone;
    }

    public void UpdateButton()
    {
        int price = _cardClone.UpgradePrice;

        if (_isUseDNA)
            _cantUpgradeVisuals.SetActive(_wallet.DNA < price);

        if (_isUseCoins)
            _cantUpgradeVisuals.SetActive(_wallet.Coins < price);
        
        _textPrice.text = NumberFormatter.DivideIntegerOnDigits(price);
    }
}
