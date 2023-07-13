using System;
using UnityEngine;
using Clones.Data;
using Random = UnityEngine.Random;
using Clones.Progression;
using System.Collections.Generic;

public class Quest : MonoBehaviour, IComplexityble
{
    [SerializeField] private QuestData _questData;
    [SerializeField] private CurrencyCounter _currencyCounter;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Complexity _complexity;

    public int Complexity => _questLevel;

    private int _questLevel;
    private List<QuestCell> _quests = new List<QuestCell>();
    private int _reward;

    public event Action ComplexityIncreased;
    public event Action<QuestCell> QuestCellUpdated;
    public event Action<IReadOnlyList<QuestCell>> QuestCreated;

    private void OnEnable()
    {
        _quests = GetQuest(out _reward);

        QuestCreated?.Invoke(_quests);

        _currencyCounter.MiningFacilityBroked += OnMiningFacilityBroked;
    }

    private void OnDisable() => _currencyCounter.MiningFacilityBroked -= OnMiningFacilityBroked;

    private void OnMiningFacilityBroked(PreyResourceType type, int count)
    {     
        bool isQuestEnded = true;

        foreach(var cell in _quests)
        {
            if(cell.Type == type && cell.IsFull == false)
            {
                cell.TryGetItems(count, type);
                //Debug.Log("required item " + count);
            }

            if(cell.IsFull == false)
                isQuestEnded = false;
        }

        if (isQuestEnded)
        {
            _wallet.TekeMoney(_reward);
            _quests = GetQuest(out _reward);

            QuestCreated?.Invoke(_quests);
        }
    }

    private List<QuestCell> GetQuest(out int reward)
    {
        _questLevel++;
        ComplexityIncreased?.Invoke();
        //Debug.Log("quest level " + _questLevel);

        List<QuestCell> cells = new List<QuestCell>();
        List<PreyResourceType> availableTypes = new List<PreyResourceType>();
        int preyResourcesTypesCount = Enum.GetNames(typeof(PreyResourceType)).Length;
        int maxItemsCount = (int)(_questData.BaseItemsCount * _complexity.ResultComplexity);
        int minItemsCount = (int)(maxItemsCount * _questData.MinimumPercentageItemCountInQuest);
        int totalItemsCount = 0;

        //Debug.Log("max items count " + maxItemsCount);
        //Debug.Log("available types " + availableTypes.Count + " preyResourcesTypes count " + preyResourcesTypesCount);
        //Debug.Log("random min " + minItemsCount + " random max " + maxItemsCount);

        while (totalItemsCount < maxItemsCount)
        {
            int itemsCount;

            if (availableTypes.Count + 1 == preyResourcesTypesCount)
            {
                itemsCount = maxItemsCount - totalItemsCount;
                //Debug.Log("get max items count");
            }
            else
            {
                //Debug.Log("get random items count");
                itemsCount = GetItemsCount(minItemsCount, maxItemsCount, totalItemsCount);
            }

            //Debug.Log("items count " + itemsCount);

            PreyResourceType type = GetUniquePreyResourceType(availableTypes, preyResourcesTypesCount);

            availableTypes.Add(type);

            cells.Add(new QuestCell(itemsCount, type));

            totalItemsCount += itemsCount;
        }

        Debug.Log("finish total count " + totalItemsCount);

        foreach (var cell in cells)
            Debug.Log(cell.Type + " " + cell.MaxCount);

        reward = (int)(_questData.BaseReward * _complexity.ResultComplexity);

        return cells;
    }

    private int GetItemsCount(int minItemsCount, int maxItemsCount, int totalItemsCount)
    {
        bool isCorrectCount = false;
        int itemsCount = 0;

        while (isCorrectCount == false)
        {
            itemsCount = Random.Range(minItemsCount, (maxItemsCount - totalItemsCount) + 1);

            //Debug.Log("random " + itemsCount);

            if (itemsCount == 0)
                isCorrectCount = false;
            else if (maxItemsCount - (itemsCount + totalItemsCount) < minItemsCount)
            {
                //Debug.Log("else if after random");
                //Debug.Log("items count " + itemsCount + " max " + maxItemsCount + " min " + minItemsCount + " total " + totalItemsCount);
                itemsCount = maxItemsCount - totalItemsCount;
                isCorrectCount = true;
            }
            else
                isCorrectCount = true;
        }

        return itemsCount;
    }

    private PreyResourceType GetUniquePreyResourceType(List<PreyResourceType> availableTypes, int preyResourcesTypesCount)
    {
        if(availableTypes.Count == preyResourcesTypesCount)
            throw new Exception("impossible to find unique objects");

        bool isUniqueType = false;
        PreyResourceType type = 0;

        while(isUniqueType == false)
        {
            type = (PreyResourceType)Random.Range(0, preyResourcesTypesCount);

            isUniqueType = true;

            foreach(var availableType in availableTypes)
            {
                if (availableType == type)
                {
                    isUniqueType = false;
                    break;
                }
            }
        }

        return type;
    }
}
