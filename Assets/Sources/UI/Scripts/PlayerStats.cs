using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Transform _playerPrefabPlace;
    [SerializeField] private Canvas _canvas;

    private int _countDNA;
    private int _countCoins;
    private float _health;
    private float _damage;
    private float _attackSpeed;
    private float _resourceMultiplier;

    private CardClone _cardClone;
    private CardWand _cardWand;

    private GameObject _clonePrefab;

    private const float _baseResourceMultiplier = 0.75f;
    private const float _upgradeResourceMultiplier = 0.25f;

    public void SelectCard(CardClone cardClone)
    {
        _cardClone = cardClone;
        UpdateStats();

        Destroy(_clonePrefab?.gameObject);
        _clonePrefab = Instantiate(_cardClone.ObjectPrefab, _playerPrefabPlace);
    }

    public void SelectCard(CardWand cardWand)
    {
        _cardWand = cardWand;
        UpdateStats();

        _cardClone?.SetWand(_cardWand.ObjectPrefab, _clonePrefab);
    }

    public void UpdateWalletValues(Wallet wallet)
    {
        _countDNA = wallet.DNA;
        _countCoins = wallet.Coins;
    }

    private void UpdateStats()
    {
        _health = _cardClone.Helath;
        _damage = _cardClone.Damage + _cardWand.Damage;
        _attackSpeed = _cardWand.AttackSpeed;
        _resourceMultiplier = (_baseResourceMultiplier + _cardClone.Level * _upgradeResourceMultiplier) * (_cardClone.BaseMultiplyRecourceByRare + _cardWand.BaseMultiplyRecourceByRare);
    }
}
