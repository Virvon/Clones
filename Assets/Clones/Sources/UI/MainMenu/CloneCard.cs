using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CloneCard : Card
{
    //[SerializeField] private UpgradeButton _upgradeByDNAButton;
    //[SerializeField] private UpgradeButton _upgradeByCoinsButton;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private GameObject _unlock;

    //private int _increaseHealth;
    //private int _increaseDamage;

    private GameObject _wandPrefab;
    private int _level = 1;

    //public int Health { get; private set; }
    //public int Damage { get; private set; }
    //public int Level => _level;
    //public int UpgradePrice { get; private set; }
    //public int IncreasePrice { get; private set; }

    public UnityEvent Selected = new UnityEvent();

    //public void Init(int health, int increaseHealth, int damage, int increaseDamage, int upgradePrice, int increasePrice)
    //{
    //    Health = health;
    //    _increaseHealth = increaseHealth;
    //    Damage = damage;
    //    _increaseDamage = increaseDamage;
    //    UpgradePrice = upgradePrice;
    //    IncreasePrice = increasePrice;
    //}

    public void SetWand(GameObject wandPrefab, GameObject clonePrefab)
    {
        Destroy(_wandPrefab?.gameObject);
        _wandPrefab = Instantiate(wandPrefab, clonePrefab.GetComponent<ClonePrafab>().WandPrefabPlace);
    }

    public void UpgradeByDNA()
    {
        //Damage += _increaseHealth;
        Upgrade();
    }

    public void UpgradeByCoins()
    {
        //Damage += _increaseDamage;
        Upgrade();
    }

    public override void Select()
    {
        base.Select();

        //PlayerView.SelectCard(this);
        UpdateUpgradeButtons();
    }

    private void UpdateUpgradeButtons()
    {
        //_upgradeByDNAButton.SetCardClone(this);
        //_upgradeByCoinsButton.SetCardClone(this);

        //_upgradeByDNAButton.UpdateButton();
        //_upgradeByCoinsButton.UpdateButton();
    }

    private void Upgrade()
    {
        _level++;
        //UpgradePrice += IncreasePrice;
        _levelText.text = _level.ToString();
        UpdateUpgradeButtons();
    }

    protected override void Unlock()
    {
        SelectButton.interactable = true;
        _unlock.SetActive(true);
    }
}
