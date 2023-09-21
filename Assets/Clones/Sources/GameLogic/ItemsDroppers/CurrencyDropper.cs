﻿using Clones.Infrastructure;
using Clones.Services;
using Clones.StaticData;
using UnityEngine;

namespace Clones.GameLogic
{
    public class CurrencyDropper : IDisable
    {
        private readonly IDestroyDroppableReporter _destroyDroppableReporter;
        private readonly IDroppableVisitor _visitor;

        public CurrencyDropper(IPartsFactory partsFactory, IDestroyDroppableReporter destroyDroppableReporter)
        {
            _visitor = new DroppableVisitor(partsFactory);

            _destroyDroppableReporter = destroyDroppableReporter;

            _destroyDroppableReporter.Destroyed += OnDestroyed;
        }

        public void OnDisable() => 
            _destroyDroppableReporter.Destroyed -= OnDestroyed;

        private void OnDestroyed(IDroppable droppable) =>
            droppable.Accept(_visitor);

        private class DroppableVisitor : IDroppableVisitor
        {
            private const float DropSpeed = 2;
            private const float DropRadius = 2;
            private const int MinPreyResourceDnaCount = 0;
            private const int MaxPreyResourceDnaCount = 3;
            private const int MinEnemyDnaCount = 1;
            private const int MaxEnemyDnaCount = 6;

            private readonly IPartsFactory _partsFactory;

            public DroppableVisitor(IPartsFactory partsFactory)
            {
                _partsFactory = partsFactory;
            }

            public void Visit(Enemy enemy)
            {
                Drop(CurrencyItemType.Dna, enemy.transform.position, MinEnemyDnaCount, MaxEnemyDnaCount);
            }

            public void Visit(PreyResource preyResource)
            {
                Drop(CurrencyItemType.Dna, preyResource.transform.position, MinPreyResourceDnaCount, MaxPreyResourceDnaCount);
            }

            private void Drop(CurrencyItemType type, Vector3 position, int minCount, int maxCount)
            {
                int count = Random.Range(minCount, maxCount + 1);

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
}
