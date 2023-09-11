using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int _countDNA;
    private int _countCoins;
    private float _health;
    private float _damage;
    private float _attackSpeed;
    private float _resourceMultiplier;

    public void SelectCardClone(CardClone cardClone)
    {
        
    }

    public void SelectCardWand(CardWand cardWand)
    {
        
    }

    public void UpdateWalletValues(Wallet wallet)
    {
        _countDNA = wallet.DNA;
        _countCoins = wallet.Coins;
    }
}
