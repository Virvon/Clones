using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private GameObject _objectPrefab;
    [Space]
    [SerializeField] private bool _isPurchased;
    [SerializeField] private GameObject _selectedVisuals;
    [SerializeField] private List<Card> _unselected—ards;
    //[SerializeField] private GameObject _dieVisuals;
    [Space]
    //[SerializeField] private int _secondsToRestore;
    [SerializeField] private float _baseMultiplyResourceByRare;

    private bool _isDead = false;

    public PlayerStats PlayerStats => _playerStats;
    public GameObject ObjectPrefab => _objectPrefab;
    public float BaseMultiplyRecourceByRare => _baseMultiplyResourceByRare;
    public bool IsPurchased => _isPurchased;

    public virtual void Select()
    {
        if(CantSelect()) 
            return;

        _selectedVisuals.SetActive(true);

        foreach (var card in _unselected—ards)
            card.Unselect();
    }

    public void Unselect()
    {
        _selectedVisuals.SetActive(false);
    }

    public void Buy()
    {
        _isPurchased = true;
    }

    public virtual bool CantSelect()
    {
        return _isDead || (_isPurchased == false);
    }
}
