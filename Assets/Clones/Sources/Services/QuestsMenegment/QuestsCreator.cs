using Clones.StaticData;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Clones.Services
{
    public class QuestsCreator : IQuestsCreator
    {
        private const int MaxItemsCoutn = 10;
        private const int MinItemsCountInQuest = 4;
        private readonly ItemType[] _questTypes = { ItemType.Green, ItemType.Blue };
        public IReadOnlyList<Quest> Quests => _quests;

        private List<Quest> _quests;

        public event Action Created;
        public event Action<Quest> Updated;


        public QuestsCreator()
        {
            _quests = GetQuests();
        }

        public bool IsQuestItem(ItemType type) => 
            _quests.Any(quest => quest.Type == type && quest.IsDone == false);

        private List<Quest> GetQuests()
        {
            List<Quest> quests = new();
            HashSet<ItemType> usedTypes = new();

            int availableTypesCount = _questTypes.Length;
            int maxItemsCount = MaxItemsCoutn;
            int minItemsCountInQuest = MinItemsCountInQuest;
            int totalItemsCount = 0;

            while (totalItemsCount < maxItemsCount)
            {
                int itemsCount;

                if (usedTypes.Count + 1 == availableTypesCount)
                    itemsCount = maxItemsCount - totalItemsCount;
                else
                    itemsCount = GetItemsCount(minItemsCountInQuest, maxItemsCount, totalItemsCount);

                ItemType type = GetUniqueType(usedTypes);

                usedTypes.Add(type);
                quests.Add(new Quest(type, itemsCount));

                totalItemsCount += itemsCount;
            }

            Created?.Invoke();

            return quests;
        }

        private ItemType GetUniqueType(HashSet<ItemType> usedTypes)
        {
            ItemType[] availableTypes = _questTypes.Except(usedTypes).ToArray();

            if (availableTypes.Length == 0)
                throw new Exception(typeof(QuestsCreator) + " cannot find unique objects");

            return availableTypes[Random.Range(0, availableTypes.Length)];
        }

        private int GetItemsCount(int minItemsCount, int maxItemsCount, int totalItemsCount)
        {
            bool isCorrectCount = false;
            int itemsCount = 0;

            while (isCorrectCount == false)
            {
                itemsCount = Random.Range(minItemsCount, (maxItemsCount - totalItemsCount) + 1);

                if (itemsCount == 0)
                {
                    isCorrectCount = false;
                }
                else if (maxItemsCount - (itemsCount + totalItemsCount) < minItemsCount)
                {
                    itemsCount = maxItemsCount - totalItemsCount;
                    isCorrectCount = true;
                }
                else
                {
                    isCorrectCount = true;
                }
            }

            return itemsCount;
        }
    }
}
