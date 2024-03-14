using Clones.Services;
using Clones.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Clones.GameLogic
{
    public class QuestsCreator : IQuestsCreator
    {
        private readonly QuestItemType[] _questTypes;
        private readonly IPersistentProgressService _persistentProgress;
        private readonly Complexity _complexity;
        private readonly float _resourcesMultiplier;
        private readonly int _itemsCount;
        private readonly int _minItemsCountPercentInQuest;
        private readonly int _reward;

        private List<Quest> _quests;
        private int _currentQuest = 0;

        public IReadOnlyList<Quest> Quests => _quests;
        public int Reward { get; private set; }

        private float Complexiy => _complexity == null ? 1 : _complexity.GetComplexity(_currentQuest);

        public event Action Created;
        public event Action<Quest> Updated;
        public event Action Completed;

        public QuestsCreator(IPersistentProgressService persistentProgress, QuestItemType[] questTypes, Complexity complexity, float resourcesMultiplier, int itemsCount, int minItemsCountPercentInQuest, int reward)
        {
            _persistentProgress = persistentProgress;
            _questTypes = questTypes;
            _complexity = complexity;
            _resourcesMultiplier = resourcesMultiplier;
            _itemsCount = itemsCount;
            _minItemsCountPercentInQuest = minItemsCountPercentInQuest;
            _reward = reward;
        }

        public QuestsCreator(IPersistentProgressService persistentProgress, QuestItemType[] questTypes, float resourcesMultiplier, int itemsCount, int minItemsCountPercentInQuest, int reward)
        {
            _persistentProgress = persistentProgress;
            _questTypes = questTypes;
            _resourcesMultiplier = resourcesMultiplier;
            _itemsCount = itemsCount;
            _minItemsCountPercentInQuest = minItemsCountPercentInQuest;
            _reward = reward;
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
            Quest updatedQuest = _quests.FirstOrDefault(quest => quest.Type == type);

            if (updatedQuest == null)
                return;

            updatedQuest.TryTakeItem(type, count);

            Updated?.Invoke(updatedQuest);

            if (_quests.All(quest => quest.IsDone))
            {
                _persistentProgress.Progress.Wallet.CollectMoney(Reward);
                Completed?.Invoke();

                Create();
            }
        }

        private List<Quest> GetQuests(out int reward)
        {
            List<Quest> quests = new();
            HashSet<QuestItemType> usedTypes = new();

            int availableTypesCount = _questTypes.Length;
            int maxItemsCount = (int)(_itemsCount * Complexiy);
            int minItemsCountInQuest = (int)(maxItemsCount * _minItemsCountPercentInQuest / 100f);
            int totalItemsCount = 0;

            _currentQuest++;

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

            reward = (int)(_reward * _resourcesMultiplier * Complexiy);

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
