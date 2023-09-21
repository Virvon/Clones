using Clones.Infrastructure;
using Clones.Services;
using Clones.StaticData;
using UnityEngine;

namespace Clones.GameLogic
{
    public class QuestItemsDropper : IDisable
    {
        private const float DropSpeed = 2;
        private const float DropRadius = 2;
        private const int MinDropCount = 1;
        private const int MaxDropCount = 4;

        private readonly IGameFactory _gameFactory;
        private readonly IDestroyDroppableReporter _destroyDroppableReporter;
        private readonly IQuestsCreator _questsCreator;

        public QuestItemsDropper(IGameFactory gameFactory, IDestroyDroppableReporter destroyDroppableReporter, IQuestsCreator questsCreator)
        {
            _gameFactory = gameFactory;
            _destroyDroppableReporter = destroyDroppableReporter;
            _questsCreator = questsCreator;

            _destroyDroppableReporter.Destroyed += OnDestroyed;
        }

        public void OnDisable() => 
            _destroyDroppableReporter.Destroyed += OnDestroyed;

        private void OnDestroyed(IDroppable droppable)
        {
            if (droppable is PreyResource preyResource)
                TryDrop(preyResource.DroppedItem, preyResource.transform.position);
        }

        private void TryDrop(QuestItemType type, Vector3 position)
        {
            if (_questsCreator.IsQuestItem(type) == false)
                return;

            int count = Random.Range(MinDropCount, MaxDropCount + 1);

            for (var i = 0; i < count; i++)
            {
                GameObject item = _gameFactory.CreateItem(type, position);

                item.GetComponent<ItemMovement>()
                    .TakeMove(GetPointInsideCircle(position), DropSpeed);
            }
        }

        private Vector3 GetPointInsideCircle(Vector3 center)
        {
            Vector2 direction = Random.insideUnitCircle.normalized;
            float distance = Random.Range(0, DropRadius + 1);
            Vector3 position = center + new Vector3(direction.x, 0, direction.y) * distance;

            return position;
        }
    }
}
