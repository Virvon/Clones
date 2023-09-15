using Clones.Infrastructure;
using Clones.StaticData;
using UnityEngine;

namespace Clones.GameLogic
{
    public class ItemsDropper : IItemsDropper
    {
        private const float DropSpeed = 2;
        private const float DropRadius = 2;

        private readonly IDestroyDroppableReporter _destroyDroppableReporter;
        private readonly IDroppableVisitor _visitor;

        public ItemsDropper(IGameFactory gameFactory, IDestroyDroppableReporter destroyDroppableReporter)
        {
            _visitor = new DroppableVisitor(gameFactory);

            _destroyDroppableReporter = destroyDroppableReporter;

            _destroyDroppableReporter.Destroyed += Drop;
        }

        private void Drop(IDroppable droppable) =>
            droppable.Accept(_visitor);

        private class DroppableVisitor : IDroppableVisitor
        {
            private readonly IGameFactory _gameFactory;

            public DroppableVisitor(IGameFactory gameFactory)
            {
                _gameFactory = gameFactory;
            }

            public void Visit(Enemy enemy)
            {

            }

            public void Visit(PreyResource preyResource)
            {
                Drop(preyResource.DroppedItem, preyResource.transform.position, 1, 5);
            }

            private void Drop(ItemType type, Vector3 position, int minCount, int maxCount)
            {
                int count = Random.Range(minCount, maxCount + 1);

                for (var i = 0; i < count; i++)
                {
                    GameObject item = _gameFactory.CreateItem(type, position);

                    item.GetComponent<Item>()
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
}