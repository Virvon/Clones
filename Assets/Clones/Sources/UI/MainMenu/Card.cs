using UnityEngine;

public abstract class Card : MonoBehaviour
{
    [SerializeField] private GameObject _selectedVisuals;
    [Space]
    [SerializeField] private float _baseMultiplyResourceByRare;

    public float BaseMultiplyRecourceByRare => _baseMultiplyResourceByRare;

    public void Start() => 
        Unselect();

    public virtual void Select() =>
        _selectedVisuals.SetActive(true);

    public void Unselect() => 
        _selectedVisuals.SetActive(false);
}