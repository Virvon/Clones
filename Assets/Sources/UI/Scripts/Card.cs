using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private int _cost;
    [SerializeField] private bool _useDNA;
    [SerializeField] private bool _useCoins;
    [Space]
    [SerializeField] private bool _isAvailableForPurchase;
    [SerializeField] private bool _isPurchased;
    [Space]
    [SerializeField] private GameObject _canBuyVisuals;
    [SerializeField] private GameObject _lockVisuals;
    [Space]
    [SerializeField] private GameObject _selectedVisuals;
    [SerializeField] private List<Card> _unselectedÑards;
    [Space]
    [SerializeField] private int _secondsToRestore;
    [SerializeField] private float _baseMultiplyRecourceByRare;
    [Space]
    [SerializeField] private Player _player;
    [SerializeField] private Wallet _wallet;

    private int _level;
    private bool _isDead;

    public int Level => _level;
    public float BaseMultiplyRecourceByRare => _baseMultiplyRecourceByRare;

    public void Select()
    {
        if (_isPurchased == false && _isDead)
            return;

        _selectedVisuals.SetActive(true);

        foreach (var card in _unselectedÑards)
            card.Unselect();
    }

    public void Unselect()
    {
        _selectedVisuals.SetActive(false);
    }

    public void Purchase()
    {
        if (_isAvailableForPurchase == false)
            return;

        if (_useDNA)
            _wallet.ChangeDNACount(-_cost);

        if (_useCoins)
            _wallet.ChangeCoinsCount(-_cost);

        _lockVisuals.gameObject.SetActive(false);
        _isPurchased = true;
    }

    public void CheckAvailabilityPurchase()
    {
        if (_useDNA)
            _isAvailableForPurchase = _wallet.DNA >= _cost;

        if (_useCoins)
            _isAvailableForPurchase = _wallet.Coins >= _cost;

        _canBuyVisuals.gameObject.SetActive(_isAvailableForPurchase);
    }
}
