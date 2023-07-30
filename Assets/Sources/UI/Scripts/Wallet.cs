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

    public int DNA => _dna;
    public int Coins => _coins;

    public void ChangeDNACount(int value)
    {
        _dna += value;
        UpdateTexts();
    }

    public void ChangeCoinsCount(int value)
    {
        _coins += value;
        UpdateTexts();
    }
    
    public void UpdateTexts()
    {
        _dnaText.text = NumberFormatter.FormatNumberWithCommas(_dna);
        _coinsText.text = NumberFormatter.FormatNumberWithCommas(_coins);
    }
}
