using UnityEngine;
using UnityEngine.UI;

public abstract class Card : MonoBehaviour
{
    [SerializeField] private GameObject _selectedVisuals;
    [SerializeField] private GameObject _lock;
    [SerializeField] private Button _select;

    protected Button SelectButton => _select;

    public void Init(bool isBuyed)
    {
        Unselect();

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

    public void Select() => 
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