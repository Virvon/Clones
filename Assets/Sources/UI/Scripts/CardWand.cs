using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardWand : MonoBehaviour, IPurchasable
{
    [SerializeField] private int _price;
    [SerializeField] private bool _isPurchased;
    [SerializeField] private GameObject _unlockPanel;
    [SerializeField] private GameObject _buyPanel;
    [SerializeField] private GameObject _frameFocus;
    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed;


    public int Price => _price;
    public bool IsPurchased => _isPurchased;
    public int Damage => _damage;
    public float AttackSpeed => _attackSpeed;

    public UnityEvent Buyed;
    public UnityEvent Selected;

    public void SwitchWisiblePanels(bool isActiveUnlockPanel, bool isActiveBuyPanel)
    {
        _unlockPanel.SetActive(isActiveUnlockPanel);
        _buyPanel.SetActive(isActiveBuyPanel);
    }

    public void Buy()
    {
        _isPurchased = true;
        Buyed.Invoke();
    }

    public bool ReturnIsPurchased()
    {
        return _isPurchased;
    }

    public void Select(bool isVisible)
    {
        _frameFocus.SetActive(isVisible);
        Selected.Invoke();
    }
}
