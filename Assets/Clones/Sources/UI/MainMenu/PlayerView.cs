using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform _playerPrefabPlace;
    [Space]
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _damageText;
    [SerializeField] private TMP_Text _attackSpeedText;
    [SerializeField] private TMP_Text _resourceMultiplierText;
    [Space]
    [SerializeField] private float _baseResourceMultiplier = 0.9f;
    [SerializeField] private float _upgradeResourceMultiplier = 0.1f;

    private GameObject _clonePrefab;
    //private CardClone _currentC

    public void SelectCard(CloneCard cardClone)
    {
        //_cardClone = cardClone;
        UpdateStats();

        Destroy(_clonePrefab?.gameObject);
        //_clonePrefab = Instantiate(_cardClone.ObjectPrefab, _playerPrefabPlace);
        //_cardClone.SetWand(_cardWand.ObjectPrefab, _clonePrefab);
    }

    public void SelectCard(WandCard cardWand)
    {
        //_cardWand = cardWand;
        UpdateStats();

        //_cardClone.SetWand(_cardWand.ObjectPrefab, _clonePrefab);
    }

    public void UpdateStats()
    {
        //int health = _cardClone.Health;
        //int damage = _cardClone.Damage + _cardWand.Damage;
        //float attackSpeed = _cardWand.AttackSpeed;
        //float resourceMultiplier = (_baseResourceMultiplier + _cardClone.Level * _upgradeResourceMultiplier) * (_cardClone.BaseMultiplyRecourceByRare + _cardWand.BaseMultiplyRecourceByRare);

        //_healthText.text = NumberFormatter.DivideIntegerOnDigits(health);
        //_damageText.text = NumberFormatter.DivideIntegerOnDigits(damage);
        //_attackSpeedText.text = NumberFormatter.DivideFloatOnDigits(attackSpeed);
        //_resourceMultiplierText.text = NumberFormatter.DivideFloatOnDigits(resourceMultiplier);
    }
}
