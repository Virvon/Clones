using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeClone : MonoBehaviour
{
    [SerializeField] private CardClone _cardClone;
    [Space]
    [Header("Health")]
    [SerializeField] private Button _healthUpgrade;
    [SerializeField] private int _priceHealthUpgrade;
    [SerializeField] private int _priceIncreaseHealthUpgrade;
    [SerializeField] private int _CountHealthUpgrade;
    [Space]
    [Header("Wand")]
    [SerializeField] private Button _wandUpgrade;
    [SerializeField] private int _priceWandUpgrade;
    [SerializeField] private int _priceIncreaseWandUpgrade;
    [SerializeField] private int _CountDamageUpgrade;
    [SerializeField] private float _CountAttackSpeedUpgrade;
    [SerializeField] private float _CountResourceMultiplierUpgrade;

    public void UpgrageWand()
    {
        //_dictionaryCharacters.Character.Upgrade();
    }

    public void UpgrageHealth()
    {
        //_dictionaryCharacters.Character.Upgrade();
    }
}
