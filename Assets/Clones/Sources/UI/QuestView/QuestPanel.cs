using Clones.Infrastructure;
using Clones.Services;
using Clones.StaticData;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.UI
{
    public class QuestPanel : MonoBehaviour
    {
        private IQuestsCreator _questsCreator;
        private IGameFactory _gameFactory;

        private Dictionary<QuestItemType, QuestView> _questViews = new();

        private void OnDisable()
        {
            _questsCreator.Created -= OnQuestCreated;
            _questsCreator.Updated -= OnQuestCellUpdated;
        }

        public void Init(IQuestsCreator questsCreator, IGameFactory gameFactory)
        {
            _questsCreator = questsCreator;
            _gameFactory = gameFactory;

            _questsCreator.Created += OnQuestCreated;
            _questsCreator.Updated += OnQuestCellUpdated;
        }

        private void OnQuestCreated()
        {
            Clear();


            foreach (var quest in _questsCreator.Quests)
            {
                GameObject view = _gameFactory.CreateQuestView(quest, transform);

                _questViews.Add(quest.Type, view.GetComponent<QuestView>());
            }
        }

        private void OnQuestCellUpdated(Quest quest)
        {
            QuestView view = _questViews[quest.Type];

            view.UpdateInfo();
        }

        private void Clear()
        {
            foreach(var view in _questViews.Values)
                Destroy(view.gameObject);

            _questViews.Clear();
        }
    }
}
