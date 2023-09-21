using Clones.Infrastructure;
using System;

namespace Clones.GameLogic
{
    public class DestroyDroppableReporter : IDestroyDroppableReporter
    {
        private readonly IPartsFactory _partsFactory;

        public event Action<IDroppable> Destroyed;

        public DestroyDroppableReporter(IPartsFactory partsFactory)
        {
            _partsFactory = partsFactory;

            _partsFactory.DroppableCreated += OnDroppableCreated;
        }
        public void OnDisable() => 
            _partsFactory.DroppableCreated -= OnDroppableCreated;

        private void OnDroppableCreated(IDroppable droppable) => 
            droppable.Died += OnDroppableDied;

        private void OnDroppableDied(IDamageable damageable)
        {
            if (damageable is IDroppable droppable)
            {
                Destroyed?.Invoke(droppable);
                droppable.Died -= OnDroppableDied;
            }
        }
    }
}