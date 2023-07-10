using System;
using UnityEngine;
using Clones.Data;
using Random = UnityEngine.Random;

public class Quest : MonoBehaviour
{
    [SerializeField] private QuestData _questData;
    [SerializeField] private CurrencyCounter _currencyCounter;
    [SerializeField] private Wallet _wallet;

    public PreyResourceType s_MiningFacilityType { get; private set; } 

    public int TargetResourcesCount => _questData.BaseItemsCount;

    public int ResourcesCount { get; private set; }

    public event Action ResourcesCountChanged;

    private void OnEnable()
    {
        s_MiningFacilityType = GetQuest();
        _currencyCounter.MiningFacilityBroked += OnMiningFacilityBroked;
    }

    private void OnDisable() => _currencyCounter.MiningFacilityBroked -= OnMiningFacilityBroked;

    private void OnMiningFacilityBroked(PreyResourceType type)
    {
        if (s_MiningFacilityType == type)
            ResourcesCount++;

        if(ResourcesCount >= TargetResourcesCount)
        {
            s_MiningFacilityType = GetQuest();
            ResourcesCount = 0;
            _wallet.TekeMoney(1);
        }

        ResourcesCountChanged?.Invoke();
    }

    private PreyResourceType GetQuest()
    {
        int miningFacilictyTypes = Random.Range(0, 3);

        return (PreyResourceType)miningFacilictyTypes;
    }
}
