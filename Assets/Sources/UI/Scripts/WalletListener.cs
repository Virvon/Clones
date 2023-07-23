using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WalletListener : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private NumberFormatter _numberFormatter;

    private void Start()
    {
        _wallet.ChangedDNACount.AddListener(UpdateText);
        _wallet.ChangedCoinsCount.AddListener(UpdateText);
    }

    private void UpdateText(int value) => _text.text = _numberFormatter.FormatNumberWithCommas(value);
}
