using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : MonoBehaviour
{
    public int DNA { get; private set; }
    public int Coins { get; private set; }

    public UnityEvent<int> ChangedDNACount = new UnityEvent<int>();
    public UnityEvent<int> ChangedCoinsCount = new UnityEvent<int>();

    public void ChangeDNACount(int value)
    {
       DNA += value;
       ChangedDNACount.Invoke(DNA); 
    }

    public void ChangeCoinsCount(int value)
    {
        Coins += value;
        ChangedCoinsCount.Invoke(Coins);
    }
}
