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

    public UnityEvent ValuesChanged = new UnityEvent();

    public int DNA => _dna;
    public int Coins => _coins;

    private void Start()
    {
        ValuesChanged.AddListener(UpdateTexts);
        ValuesChanged.Invoke();
        UpdateTexts(); // In canvas
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
        _dnaText.text = NumberFormatter.DivideIntegerOnDigits(_dna);
        _coinsText.text = NumberFormatter.DivideIntegerOnDigits(_coins);
    }
}
