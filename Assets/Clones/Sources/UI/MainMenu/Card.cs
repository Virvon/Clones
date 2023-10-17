using UnityEngine;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour
{
    [SerializeField] private GameObject _selectedVisuals;
    [SerializeField] private GameObject _lock;
    [SerializeField] private Button _select;

    protected Button SelectButton => _select;
    //[SerializeField] private float _baseMultiplyResourceByRare;

    //public float BaseMultiplyRecourceByRare => _baseMultiplyResourceByRare;

    public void Start() => 
        Unselect();

    public void Init(bool isBuyed)
    {
        if (isBuyed)
            Unlock();
        else
            Lock();
    }

    public void Buy()
    {
        _lock.SetActive(false);
        Unlock();
    }

    public virtual void Select() =>
        _selectedVisuals.SetActive(true);

    public void Unselect() => 
        _selectedVisuals.SetActive(false);

    protected abstract void Unlock();

    private void Lock()
    {
        _lock.SetActive(true);
        _select.interactable = false;
    }
}