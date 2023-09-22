using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Events;

public class CardClone : Card
{
    [Space]
    [Header("Характеристики клона")]
    [SerializeField] private float _helath;
    [SerializeField] private float _damage;
    [Space]
    [SerializeField] private int _upgradePrice;
    [SerializeField] private int _increasePrice;
    [Space]
    [SerializeField] private UpgradeButton _upgradeByDNAButton;
    [SerializeField] private UpgradeButton _upgradeByCoinsButton;

    private GameObject _wandPrefab;

    public float Helath => _helath;
    public float Damage => _damage;

    public UnityEvent Selected = new UnityEvent();

    public void SetWand(GameObject wandPrefab, GameObject clonePrefab)
    {
        Destroy(_wandPrefab?.gameObject);
        _wandPrefab = Instantiate(wandPrefab, clonePrefab.GetComponent<ClonePrafab>().WandPrefabPlace);
    }

    public override void Select()
    {
        base.Select();

        if (CantSelect())
            return;

        PlayerStats.SelectCard(this);
    }

    private void UpdateUpgradeButtons()
    {
        _upgradeByDNAButton.UpdateButton(_upgradePrice);
        _upgradeByCoinsButton.UpdateButton(_upgradePrice);
    }
}
