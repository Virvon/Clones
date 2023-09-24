using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Events;

public class CardClone : Card
{
    [Space]
    [Header("Характеристики клона")]
    [SerializeField] private int _helath;
    [SerializeField] private int _increaseHealth;
    [SerializeField] private int _damage;
    [SerializeField] private int _increaseDamage;
    [Space]
    [SerializeField] private int _upgradePrice;
    [SerializeField] private int _increasePrice;
    [Space]
    [SerializeField] private UpgradeButton _upgradeByDNAButton;
    [SerializeField] private UpgradeButton _upgradeByCoinsButton;

    private GameObject _wandPrefab;
    private int _level = 1;

    public int Helath => _helath;
    public int Damage => _damage;
    public int Level => _level;
    public int UpgradePrice => _upgradePrice;
    public int IncreasePrice => _increasePrice;

    public UnityEvent Selected = new UnityEvent();

    public void SetWand(GameObject wandPrefab, GameObject clonePrefab)
    {
        Destroy(_wandPrefab?.gameObject);
        _wandPrefab = Instantiate(wandPrefab, clonePrefab.GetComponent<ClonePrafab>().WandPrefabPlace);
    }

    public void UpgradeByDNA()
    {
        _helath += _increaseHealth;
        Upgrade();
    }

    public void UpgradeByCoins()
    {
        _damage += _increaseDamage;
        Upgrade();
    }

    public override void Select()
    {
        base.Select();

        if (CantSelect())
            return;

        PlayerStats.SelectCard(this);
        UpdateUpgradeButtons();
    }

    private void UpdateUpgradeButtons()
    {
        _upgradeByDNAButton.UpdateButton();
        _upgradeByCoinsButton.UpdateButton();

        _upgradeByDNAButton.SetCardClone(this);
        _upgradeByCoinsButton.SetCardClone(this);
    }

    private void Upgrade()
    {
        _level++;
        _upgradePrice += _increasePrice;
        UpdateUpgradeButtons();
    }
}
