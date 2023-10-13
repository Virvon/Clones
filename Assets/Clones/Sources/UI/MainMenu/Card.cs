using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private PlayerView _playerStats;
    [SerializeField] private GameObject _objectPrefab;
    [Space]
    [SerializeField] private GameObject _selectedVisuals;
    [SerializeField] private List<Card> _unselected—ards;
    [Space]
    [SerializeField] private float _baseMultiplyResourceByRare;

    public PlayerView PlayerStats => _playerStats;
    public GameObject ObjectPrefab => _objectPrefab;
    public float BaseMultiplyRecourceByRare => _baseMultiplyResourceByRare;

    public virtual void Select()
    {
        _selectedVisuals.SetActive(true);

        foreach (var card in _unselected—ards)
            card.Unselect();
    }

    public void Unselect()
    {
        _selectedVisuals.SetActive(false);
    }
}
