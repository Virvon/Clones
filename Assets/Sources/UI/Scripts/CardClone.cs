using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardClone : MonoBehaviour
{
    //[SerializeField] private GameObject _prefab;
    [SerializeField] private Stats _stats;
    [SerializeField] private UpgradeClone _upgradeClone;
    [SerializeField] private DisplayStats _displayStats;
    [SerializeField] private Wallet _wallet;

    public Stats Stats => _stats;
    public UpgradeClone UpgradeClone => _upgradeClone;
    public Wallet Wallet => _wallet;

    public void Selected()
    {
        _displayStats.ShowStats();
    }
}
