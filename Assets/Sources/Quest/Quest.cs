using System;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] private int _targetResourcesCount;
    [SerializeField] private CurrencyCounter _currencyCounter;
    [SerializeField] private Wallet _wallet;

    public int TargetResourcesCount => _targetResourcesCount;

    public int ResourcesCount { get; private set; }

    public event Action ResourcesCountChanged;

    private void OnEnable() => _currencyCounter.MiningFacilityBroked += OnMiningFacilityBroked;

    private void OnDisable() => _currencyCounter.MiningFacilityBroked -= OnMiningFacilityBroked;

    private void OnMiningFacilityBroked()
    {
        ResourcesCount++;

        if(ResourcesCount >= TargetResourcesCount)
        {
            ResourcesCount = 0;
            _wallet.TekeMoney(1);
        }

        ResourcesCountChanged?.Invoke();
    }
}
