using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(SwitcherVisibleUIElements))]
public class StartUI : MonoBehaviour
{
    [SerializeField] private CardClone _startSelectedCardClone;
    [SerializeField] private SwitcherVisibleUIElements _switcherVisibleUIElements;
    [SerializeField] private Wallet _wallet;

    private void Start()
    {
        Invoke();
    }

    private void Invoke()
    {
        _startSelectedCardClone.Selected();
        _switcherVisibleUIElements.SwitchVisibleObjects();
        _wallet.ValuesChanged.Invoke();
    }
}
