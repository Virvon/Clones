using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Clones.UI
{
    public class QuestPanel : MonoBehaviour
    {
        [SerializeField] private Quest _quest;
        [SerializeField] private QuestView _questViewPrefab;
        [SerializeField] private TMP_Text _rewrdValue;

        private Dictionary<PreyResourceType, QuestView> _questViews = new Dictionary<PreyResourceType, QuestView>();

        private void OnEnable()
        {
            _quest.QuestCreated += OnQuestCreated;
            _quest.QuestCellUpdated += OnQuestCellUpdated;
        }

        private void OnDisable()
        {
            _quest.QuestCreated -= OnQuestCreated;
            _quest.QuestCellUpdated -= OnQuestCellUpdated;
        }

        private void OnQuestCreated()
        {
            Clear();

            //_rewrdValue.text = _quest.Reward.ToString();

            foreach (var cell in _quest.Quests)
            {
                QuestView view = Instantiate(_questViewPrefab, transform);

                view.Init(cell);
                _questViews.Add(cell.Type, view);
            }
        }

        private void OnQuestCellUpdated(QuestCell questCell)
        {
            QuestView view = _questViews[questCell.Type];

            view.UpdateInfo();
        }

        private void Clear()
        {
            if (_questViews.Count == 0)
                return;

            foreach(var view in _questViews.Values)
            {
                Destroy(view.gameObject);
            }

            _questViews.Clear();
        }
    }
}
