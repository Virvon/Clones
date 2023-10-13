using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private GameObject _objectPrefab;
    [Space]
    [SerializeField] private GameObject _selectedVisuals;
    [SerializeField] private List<Card> _unselected—ards;
    [Space]
    [SerializeField] private float _baseMultiplyResourceByRare;

    public PlayerView PlayerView => _playerView;
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

public class CardsView : MonoBehaviour
{

}
