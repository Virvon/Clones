using Clones.Infrastructure;
using Clones.Types;
using UnityEngine;

namespace Clones.GameLogic
{
    public class QuestItemsDropper : IDisable
    {
        private const float DropSpeed = 2;
        private const float DropRadius = 2;
        private const int MinDropCount = 1;
        private const int MaxDropCount = 4;

        private readonly IPartsFactory _partsFactory;
        private readonly CharacterAttack _characterAttack;
        private readonly IQuestsCreator _questsCreator;

        public QuestItemsDropper(IPartsFactory partsFactory, CharacterAttack characterAttack, IQuestsCreator questsCreator)
        {
            _partsFactory = partsFactory;
            _characterAttack = characterAttack;
            _questsCreator = questsCreator;

            _characterAttack.Killed += OnKilled;
        }

        private void OnKilled(IDamageable damageable)
        {
            if(damageable is PreyResource preyResource)
                TryDrop(preyResource.DroppedItem, preyResource.transform.position);
        }

        public void OnDisable() =>
            _characterAttack.Killed -= OnKilled;

        private void TryDrop(QuestItemType type, Vector3 position)
        {
            if (_questsCreator.IsQuestItem(type) == false)
                return;

            int count = Random.Range(MinDropCount, MaxDropCount + 1);

            for (var i = 0; i < count; i++)
            {
                GameObject item = _partsFactory.CreateItem(type, position);

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
