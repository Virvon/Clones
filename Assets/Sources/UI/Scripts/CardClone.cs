using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardClone : MonoBehaviour, IPurchasable
{
    //[SerializeField] private GameObject _prefab;
    [SerializeField] private Stats _stats;
    [SerializeField] private UpgradeClone _upgradeClone;
    [SerializeField] private DisplayStats _displayStats;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private bool _isPurchased;
    [SerializeField] private int _price;
    [SerializeField] private GameObject _unlockPanel;
    [SerializeField] private GameObject _diePanel;
    [SerializeField] private GameObject _buyPanel;

    public Stats Stats => _stats;
    public UpgradeClone UpgradeClone => _upgradeClone;

    public DisplayStats DisplayStats => _displayStats;
    public Wallet Wallet => _wallet;
    public int Price => _price;

    public UnityEvent Buyed;

    private void Start()
    {
        if (_isPurchased)
            SwitchWisiblePanels(true, false, false);
        else
            SwitchWisiblePanels(false, false, true);
    }

    public void Selected()
    {
        _displayStats.ShowStats();
    }

    public void SwitchWisiblePanels(bool isActiveUnlockPanel, bool isActiveDiePanel, bool isActiveBuyPanel)
    {
        _unlockPanel.SetActive(isActiveUnlockPanel);
        _diePanel.SetActive(isActiveDiePanel);
        _buyPanel.SetActive(isActiveBuyPanel);
    }

    public void Buy()
    {
        _isPurchased = true;
        Buyed.Invoke();
    }

    public bool ReturnPurchased()
    {
        return _isPurchased;
    }
}
