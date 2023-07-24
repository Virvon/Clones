using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(NumberFormatter), (typeof(TextMeshPro)), typeof(Wallet))]
public abstract class WalletListener
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TextMeshPro _text;
    [SerializeField] private NumberFormatter _numberFormatter;

    private void Start()
    {
        _wallet.ChangedDNACount.AddListener(UpdateText);
        _wallet.ChangedCoinsCount.AddListener(UpdateText);
    }

    private void UpdateText(int value)
    {
        _text.text = _numberFormatter.FormatNumberWithCommas(value);
        SwitchAccessibility(value);
    }

    private void SwitchAccessibility(int value) 
    {
        
    }
}
