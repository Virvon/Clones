using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayStats : MonoBehaviour
{
    [SerializeField] private CardClone _card;
    //[SerializeField] private Vector3 _prefabPosition;
    [SerializeField] private TMP_Text _textLevel;
    [SerializeField] private TMP_Text _textHealth;
    [SerializeField] private TMP_Text _textDamage;
    [SerializeField] private TMP_Text _textAttackSpeed;
    [SerializeField] private TMP_Text _textMultiplierResources;
    [SerializeField] private TMP_Text _textSecondsRecovery;
    [SerializeField] private UpgradeCloneButton _upgradeButtonByDNA;
    [SerializeField] private UpgradeCloneButton _upgradeButtonByCoins;

    public void ShowStats()
    {
        _textLevel.text = _card.Stats.Level.ToString();
        _textHealth.text = _card.Stats.Health.ToString();
        _textDamage.text = _card.Stats.Damage.ToString();
        _textAttackSpeed.text = _card.Stats.AttackSpeed.ToString();
        _textMultiplierResources.text = _card.Stats.MultiplierResources.ToString();
        _upgradeButtonByDNA.GetClone(_card);
        _upgradeButtonByCoins.GetClone(_card);
    }
}
