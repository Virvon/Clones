using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeClone : MonoBehaviour
{
    [SerializeField] private CardClone _cardClone;
    [SerializeField] private int _priceUpgrade;
    [SerializeField] private int _incrementPriceUpgrade;
    [SerializeField] private int _countHealthUpgrade;
    [SerializeField] private int _countDamageUpgrade;
    [SerializeField] private float _countAttackSpeedUpgrade;
    [SerializeField] private float _countResourceMultiplierUpgrade;

    public int PriceUpgrade => _priceUpgrade;

    public void UpgrageHealth()
    {
        _cardClone.Wallet.ChangeDNACount(-_priceUpgrade);
        _priceUpgrade += _incrementPriceUpgrade;
        _cardClone.Stats.Upgrade(_countHealthUpgrade, 0, 0, 0);
        _cardClone.DisplayStats.ShowStats();
    }

    public void UpgrageWand()
    {
        _cardClone.Wallet.ChangeCoinsCount(-_priceUpgrade);
        _priceUpgrade += _incrementPriceUpgrade;
        _cardClone.Stats.Upgrade(0, _countDamageUpgrade, _countAttackSpeedUpgrade, _countResourceMultiplierUpgrade);
        _cardClone.DisplayStats.ShowStats();
    }
}
