using Clones.Services;
using Clones.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.GameLogic
{
    public class EducationQuestsCreator : IQuestsCreator
    {
        private readonly Quest[][] _allQuests;
        private readonly int _rewardIncrease;
        private readonly IPersistentProgressService _persistentProgress;

        private int _reward;
        private List<Quest> _currentQuests;
        private int _questNumber;

        public int Reward { get; private set; }
        public float Complexity { get; private set; }
        public IReadOnlyList<Quest> Quests => _currentQuests;


        public event Action Created;
        public event Action<Quest> Updated;
        public event Action Completed;

        public EducationQuestsCreator(Quest[][] allQuests, int reward, int rewardIncrease, IPersistentProgressService persistentProgress)
        {
            _allQuests = allQuests;
            _reward = reward;
            _rewardIncrease = rewardIncrease;
            _persistentProgress = persistentProgress;

            _questNumber = 0;
        }

        public void Create()
        {
            Reward = _reward;
            _reward += _rewardIncrease;

            _currentQuests = _allQuests[_questNumber].ToList();
            _questNumber++;
            Created?.Invoke();
        }

        public bool IsQuestItem(QuestItemType type) =>
            _currentQuests.Any(quest => quest.Type == type && quest.IsDone == false);

        public void TakeItem(QuestItemType type, int count)
        {
            Quest updatedQuest = _currentQuests.FirstOrDefault(quest => quest.Type == type);

            if (updatedQuest == null)
                return;

            updatedQuest.TryTakeItem(type, count);

            Updated?.Invoke(updatedQuest);

            if (_currentQuests.All(quest => quest.IsDone))
            {
                _persistentProgress.Progress.Wallet.CollectMoney(Reward);
                Completed?.Invoke();

                Create();
            }
        }
    }
}