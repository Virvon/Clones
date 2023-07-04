using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public int Money {get; private set;}
    public int DNA {get; private set;}

    public event Action MoneyCountChanged;
    public event Action DNACountChanged;

    public void TekeMoney(int moneyCount)
    {
        Money += moneyCount;
        MoneyCountChanged?.Invoke();
    }

    public void TakeDNA(int DNACount)
    {
        DNA += DNACount;
        DNACountChanged?.Invoke();
    }
}
