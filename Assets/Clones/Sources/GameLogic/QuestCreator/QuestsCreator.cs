using Clones.Services;
using Clones.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Clones.GameLogic
{
    public class QuestsCreator : IQuestsCreator
    {
        private const int MaxItemsCoutn = 10;
        private const int MinItemsCountInQuest = 4;
        private const int MaxReward = 10;

        private readonly QuestItemType[] _questTypes;
        private readonly IPersistentProgressService _persistentProgress;

        private List<Quest> _quests;

        public IReadOnlyList<Quest> Quests => _quests;
        public int Reward { get; private set; }

        public event Action Created;
        public event Action<Quest> Updated;

        public QuestsCreator(IPersistentProgressService persistentProgress, QuestItemType[] questTypes)
        {
            _persistentProgress = persistentProgress;
            _questTypes = questTypes;
        }

        public void Create()
        {
            _quests = GetQuests(out int reward);
            Reward = reward;

            Created?.Invoke();
        }

        public bool IsQuestItem(QuestItemType type) => 
            _quests.Any(quest => quest.Type == type && quest.IsDone == false);

        public void TakeItem(QuestItemType type, int count)
        {
            Debug.Log("name " + type + " taked");

            var updatedQuest = _quests.First(quest => quest.Type == type);
            updatedQuest.TryTakeItem(type, count);

            Updated?.Invoke(updatedQuest);

            if (_quests.All(quest => quest.IsDone))
            {
                _persistentProgress.Progress.Wallet.CollectMoney(Reward);

                Create();
            }
        }

        private List<Quest> GetQuests(out int reward)
        {
            List<Quest> quests = new();
            HashSet<QuestItemType> usedTypes = new();

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

                QuestItemType type = GetUniqueType(usedTypes);

                usedTypes.Add(type);
                quests.Add(new Quest(type, itemsCount));

                totalItemsCount += itemsCount;
            }

            reward = MaxReward;

            return quests;
        }

        private QuestItemType GetUniqueType(HashSet<QuestItemType> usedTypes)
        {
            QuestItemType[] availableTypes = _questTypes.Except(usedTypes).ToArray();

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
