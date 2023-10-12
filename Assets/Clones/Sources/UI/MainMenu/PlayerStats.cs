using Clones.Max;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Transform _playerPrefabPlace;
    [SerializeField] private Wallet _wallet;
    [Space]
    [SerializeField] private CardClone _cardClone;
    [SerializeField] private CardWand _cardWand;
    [Space]
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _attackSpeedText;
    [SerializeField] private TMP_Text _resourceMultiplierText;
    [Space]
    [SerializeField] private float _baseResourceMultiplier = 0.9f;
    [SerializeField] private float _upgradeResourceMultiplier = 0.1f;

    private int _countDNA;
    private int _countCoins;
    private int _health;
    private int _damage;
    private float _attackSpeed;
    private float _resourceMultiplier;

    private GameObject _clonePrefab;

    private void Start()
    {
        _cardClone.Select();
        _cardWand.Select();
    }

    public void SelectCard(CardClone cardClone)
    {
        _cardClone = cardClone;
        UpdateStats();

        Destroy(_clonePrefab?.gameObject);
        _clonePrefab = Instantiate(_cardClone.ObjectPrefab, _playerPrefabPlace);
        _cardClone.SetWand(_cardWand.ObjectPrefab, _clonePrefab);
    }

    public void SelectCard(CardWand cardWand)
    {
        _cardWand = cardWand;
        UpdateStats();

        _cardClone.SetWand(_cardWand.ObjectPrefab, _clonePrefab);
    }

    public void UpdateStats()
    {
        _health = _cardClone.Helath;
        _damage = _cardClone.Damage + _cardWand.Damage;
        _attackSpeed = _cardWand.AttackSpeed;
        _resourceMultiplier = (_baseResourceMultiplier + _cardClone.Level * _upgradeResourceMultiplier) * (_cardClone.BaseMultiplyRecourceByRare + _cardWand.BaseMultiplyRecourceByRare);

        _healthText.text = NumberFormatter.DivideIntegerOnDigits(_health);
        _damageText.text = NumberFormatter.DivideIntegerOnDigits(_damage);
        _attackSpeedText.text = NumberFormatter.DivideFloatOnDigits(_attackSpeed);
        _resourceMultiplierText.text = NumberFormatter.DivideFloatOnDigits(_resourceMultiplier);

        UpdateWalletValues();
    }

    public void UpdateWalletValues()
    {
        _countDNA = _wallet.DNA;
        _countCoins = _wallet.Coins;
    }
}
