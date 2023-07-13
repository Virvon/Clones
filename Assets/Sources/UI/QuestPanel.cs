using System.Collections.Generic;
using UnityEngine;

namespace Clones.UI
{
    public class QuestPanel : MonoBehaviour
    {
        [SerializeField] private Quest _quest;
        [SerializeField] private QuestView _questViewPrefab;

        private Dictionary<PreyResourceType, QuestView> _questViews;

        private void OnEnable()
        {
            _quest.QuestCreated += OnQuestCreated;
        }

        private void OnDisable()
        {
            _quest.QuestCreated -= OnQuestCreated;
        }

        private void OnQuestCreated(IReadOnlyList<QuestCell> cells)
        {
            if(_questViews.Count != 0)
            {
                foreach (var view in _questViews)
                    Destroy(view.Value);
            }

            foreach(var cell in cells)
            {
                QuestView view = Instantiate(_questViewPrefab, transform);

                view.Init(cell);
                _questViews.Add(cell.Type, view);
            }
        }

        private void OnQuestCellUpdated()
        {

        }
    }
}
