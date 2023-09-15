using System;
using UnityEngine;
using Clones.Data;
using Random = UnityEngine.Random;
using Clones.Progression;
using System.Collections.Generic;
using Clones.StaticData;

public class Quest : MonoBehaviour, IComplexityble
{
    [SerializeField] private QuestData _questData;
    [SerializeField] private CurrencyCounter _currencyCounter;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Complexity _complexity;
    //[SerializeField] private Player _player;

    public int QuestLevel => _questLevel;
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

    public bool IsQuestItem(ItemStaticData itemData)
    {
        foreach(var cell in _quests)
        {
            if(cell.Type == itemData && cell.IsFull == false)
                return true;
        }

        return false;
    }

    public void TakePreyResourceItem(ItemStaticData itemData, int count)
    {
        bool isQuestEnded = true;

        foreach (var cell in _quests)
        {
            if (cell.Type == itemData && cell.IsFull == false)
            {
                if (cell.TryGetItems(itemData, count))
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
        List<ItemStaticData> usedItems = new List<ItemStaticData>();

        int availableItemsCount = _questData.QuestItemDatas.Count;
        int maxItemsCount = (int)(_questData.ItemsCount * _complexity.Value);
        int minItemsCount = (int)(maxItemsCount * _questData.MinimumPercentageItemCountInQuest);
        int totalItemsCount = 0;

        while(totalItemsCount < maxItemsCount)
        {
            int itemsCount;

            if(usedItems.Count + 1 == availableItemsCount)
                itemsCount = maxItemsCount - totalItemsCount;
            else
                itemsCount = GetItemsCount(minItemsCount, maxItemsCount, totalItemsCount);

            ItemStaticData itemData = GetUniqueItem(usedItems, availableItemsCount);

            usedItems.Add(itemData);
            cells.Add(new QuestCell(itemData, itemsCount));

            totalItemsCount += itemsCount;
        }

        //reward = (int)(totalItemsCount * _questData.Reward * _player.ResourceMultiplier);
        reward = 0;

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

    private ItemStaticData GetUniqueItem(List<ItemStaticData> usedItems, int avaliableItemsCount)
    {
        if (usedItems.Count == avaliableItemsCount)
            throw new Exception("impossible to find unique objects");

        bool isUniqueType = false;
        ItemStaticData item = null;

        while (isUniqueType == false)
        {
            item = _questData.QuestItemDatas[Random.Range(0, avaliableItemsCount)];

            isUniqueType = true;

            foreach (var usedItem in usedItems)
            {
                if (usedItem == item)
                {
                    isUniqueType = false;
                    break;
                }
            }
        }

        return item;
    }
}
