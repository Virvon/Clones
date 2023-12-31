﻿using Clones.GameLogic;
using UnityEngine;

public class CurrencyItem : MonoBehaviour, IItem
{
    public void Accept(IItemVisitor visitor) => 
        visitor.Visit(this);
}
