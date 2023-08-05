using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    [SerializeField] private int _dna;
    [SerializeField] private int _coins;
    [SerializeField] private TMP_Text _dnaText;
    [SerializeField] private TMP_Text _coinsText;

    public UnityEvent ValuesChanged;

    public int DNA => _dna;
    public int Coins => _coins;

    private void Start()
    {
        ValuesChanged.Invoke();
    }

    public void ChangeDNACount(int value)
    {
        _dna += value;
        ValuesChanged.Invoke();
    }

    public void ChangeCoinsCount(int value)
    {
        _coins += value;
        ValuesChanged.Invoke();
    }
    
    public void UpdateTexts()
    {
        _dnaText.text = NumberFormatter.FormatNumberWithCommas(_dna);
        _coinsText.text = NumberFormatter.FormatNumberWithCommas(_coins);
    }
}
