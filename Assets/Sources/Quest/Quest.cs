using System;
using UnityEngine;
using Clones.Data;
using Random = UnityEngine.Random;
using Clones.Progression;
using System.Collections.Generic;

public class Quest : MonoBehaviour, IComplexityble
{
    [SerializeField] private List<PreyResourceData> _resourceDatas;
    [SerializeField] private QuestData _questData;
    [SerializeField] private CurrencyCounter _currencyCounter;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Complexity _complexity;

    public int Complexity => _questLevel;
    public IReadOnlyList<QuestCell> Quests => _quests;
    public int Reward => _reward;

    private int _questLevel;
    private List<QuestCell> _quests = new List<QuestCell>();
    private int _reward;

    public event Action ComplexityIncreased;
    public event Action<QuestCell> QuestCellUpdated;
    public event Action QuestCreated;

    private void OnEnable()
    {
        _quests = GetQuest(out _reward);

        QuestCreated?.Invoke();
    }

    public bool TryGetPreyResourceData(out PreyResourceData data, PreyResource preyResource)
    {
        foreach (var cell in _quests)
        {
            if (cell.Type == preyResource.Type && cell.IsFull == false)
            {
                foreach(var resourceData in _resourceDatas)
                {
                    if(resourceData.Type == preyResource.Type)
                    {
                        data = resourceData;
                        return true;
                    }
                }
            }
        }

        data = null;
        return false;
    }

    public void TakePreyResourceItem(PreyResourceType type, int count)
    {
        bool isQuestEnded = true;

        foreach (var cell in _quests)
        {
            if(cell.Type == type && cell.IsFull == false)
            {
                if (cell.TryGetItems(count, type))
                    QuestCellUpdated?.Invoke(cell);
            }

            if (cell.IsFull == false)
                isQuestEnded = false;
        }

        if (isQuestEnded)
        {
            _wallet.TekeMoney(_reward);
            _quests = GetQuest(out _reward);

            QuestCreated?.Invoke();
        }
    }

    private List<QuestCell> GetQuest(out int reward)
    {
        _questLevel++;
        ComplexityIncreased?.Invoke();

        List<QuestCell> cells = new List<QuestCell>();
        List<PreyResourceType> availableTypes = new List<PreyResourceType>();
        int preyResourcesTypesCount = Enum.GetNames(typeof(PreyResourceType)).Length;
        int maxItemsCount = (int)(_questData.BaseItemsCount * _complexity.ResultComplexity);
        int minItemsCount = (int)(maxItemsCount * _questData.MinimumPercentageItemCountInQuest);
        int totalItemsCount = 0;

        while (totalItemsCount < maxItemsCount)
        {
            int itemsCount;

            if (availableTypes.Count + 1 == preyResourcesTypesCount)
                itemsCount = maxItemsCount - totalItemsCount;
            else
                itemsCount = GetItemsCount(minItemsCount, maxItemsCount, totalItemsCount);

            PreyResourceType type = GetUniquePreyResourceType(availableTypes, preyResourcesTypesCount);

            availableTypes.Add(type);

            cells.Add(new QuestCell(itemsCount, type));

            totalItemsCount += itemsCount;
        }

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

            if (itemsCount == 0)
                isCorrectCount = false;
            else if (maxItemsCount - (itemsCount + totalItemsCount) < minItemsCount)
            {
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
