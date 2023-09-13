using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    [SerializeField] private TMP_Text _walletDNA;
    [SerializeField] private TMP_Text _walletCoins;
    [Space]
    [SerializeField] private TMP_Text _statsHealth;
    [SerializeField] private TMP_Text _statsDamage;
    [SerializeField] private TMP_Text _statsAttackSpeed;
    [SerializeField] private TMP_Text _statsResourceMultiplier;
    [Space]
    [SerializeField] private TMP_Text _upgradeButtonDNAPrice;
    [SerializeField] private TMP_Text _upgradeButtonCoinPrice;
}
