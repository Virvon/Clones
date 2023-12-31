﻿using Clones.GameLogic;
using Clones.Infrastructure;
using Clones.Types;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class QuestPanel : MonoBehaviour
    {
        [SerializeField] private TMP_Text _rewardValue;

        private IQuestsCreator _questsCreator;
        private IUiFactory _uiFactory;

        private Dictionary<QuestItemType, QuestView> _questViews = new();

        private void OnDisable()
        {
            _questsCreator.Created -= OnQuestCreated;
            _questsCreator.Updated -= OnQuestCellUpdated;
        }

        public void Init(IQuestsCreator questsCreator, IUiFactory uiFactory)
        {
            _questsCreator = questsCreator;
            _uiFactory = uiFactory;

            _questsCreator.Created += OnQuestCreated;
            _questsCreator.Updated += OnQuestCellUpdated;
        }

        private void OnQuestCreated()
        {
            Clear();

            foreach (var quest in _questsCreator.Quests)
            {
                GameObject view = _uiFactory.CreateQuestView(quest, transform);

                _questViews.Add(quest.Type, view.GetComponent<QuestView>());
            }

            _rewardValue.text = _questsCreator.Reward.ToString();
        }

        private void OnQuestCellUpdated(Quest quest)
        {
            QuestView view = _questViews[quest.Type];

            view.UpdateInfo();
        }

        private void Clear()
        {
            foreach (var view in _questViews.Values)
                Destroy(view.gameObject);

            _questViews.Clear();
            _rewardValue.text = "";
        }
    }
}