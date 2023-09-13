using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour //Написать последовательность действий > сделать функционал по требованиям (Нажал на карту, нажал прокачать...)
{
    [SerializeField] private int _buyPrice;
    [SerializeField] private int _startUpgradePrice;
    [SerializeField] private int _increasePrice;
    [SerializeField] private bool _useDNA;
    [SerializeField] private bool _useCoins;
    [Space]
    [SerializeField] private bool _isPurchased;
    [SerializeField] private bool _isAvailableForPurchase;
    [Space]
    [SerializeField] private GameObject _canBuyVisuals;
    [SerializeField] private GameObject _cantBuyVisuals;
    [SerializeField] private TMP_Text _textPrice;
    [Space]
    [SerializeField] private GameObject _selectedVisuals;
    [SerializeField] private GameObject _dieVisuals;
    [SerializeField] private List<Card> _unselectedСards;
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

    private void Start()
    {
        if (_textPrice != null)
            _textPrice.text = _buyPrice.ToString();

        if (_isPurchased)
        {
            _canBuyVisuals.SetActive(false);
            _cantBuyVisuals.SetActive(false);
        }

        CheckAvailabilityPurchase();
    }

    public void Select()
    {
        if (_isPurchased == false && _isDead)
            return;

        _selectedVisuals.SetActive(true);

        foreach (var card in _unselectedСards)
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
            _wallet.ChangeDNACount(-_buyPrice);

        if (_useCoins)
            _wallet.ChangeCoinsCount(-_buyPrice);

        _cantBuyVisuals.gameObject.SetActive(false);
        _isPurchased = true;
    }

    public void CheckAvailabilityPurchase()
    {
        if (_isPurchased)
            return;

        if (_useDNA)
            _isAvailableForPurchase = _wallet.DNA >= _buyPrice;

        if (_useCoins)
            _isAvailableForPurchase = _wallet.Coins >= _buyPrice;

        _canBuyVisuals.gameObject.SetActive(_isAvailableForPurchase);
    }
}
