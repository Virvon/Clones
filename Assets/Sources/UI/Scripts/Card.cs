using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private GameObject _objectPrefab;
    [Space]
    [SerializeField] private int _startUpgradePrice;
    [SerializeField] private int _increasePrice;
    [SerializeField] private bool _useDNA;
    [SerializeField] private bool _useCoins;
    [Space]
    [SerializeField] private bool _isPurchased;
    [SerializeField] private GameObject _selectedVisuals;
    [SerializeField] private GameObject _dieVisuals;
    [SerializeField] private List<Card> _unselected—ards;
    [Space]
    [SerializeField] private int _secondsToRestore;
    [SerializeField] private float _baseMultiplyResourceByRare;

    private int _level;
    private bool _isDead = false;

    public GameObject ObjectPrefab => _objectPrefab;
    public int Level => _level;
    public float BaseMultiplyRecourceByRare => _baseMultiplyResourceByRare;

    private void Start()
    {

    }

    public void Select()
    {
        if (_isDead == false && _isPurchased)
        {
            _selectedVisuals.SetActive(true);

            foreach (var card in _unselected—ards)
                card.Unselect();
        }
    }

    public void Unselect()
    {
        _selectedVisuals.SetActive(false);
    }

    public void Buy()
    {
        _isPurchased = true;
    }
}
