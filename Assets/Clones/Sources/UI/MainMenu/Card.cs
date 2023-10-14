using UnityEngine;

public abstract class Card : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private GameObject _objectPrefab;
    [Space]
    [SerializeField] private GameObject _selectedVisuals;
    [Space]
    [SerializeField] private float _baseMultiplyResourceByRare;

    public bool CanSelected = true;
    public PlayerView PlayerView => _playerView;
    public GameObject ObjectPrefab => _objectPrefab;
    public float BaseMultiplyRecourceByRare => _baseMultiplyResourceByRare;

    public void Start() => 
        Unselect();

    public virtual void Select() =>
        _selectedVisuals.SetActive(true);

    public void Unselect() => 
        _selectedVisuals.SetActive(false);
}