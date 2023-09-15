using Clones.Infrastructure;
using System;

namespace Clones.GameLogic
{
    public class DestroyDroppableReporter : IDestroyDroppableReporter
    {
        private readonly IGameFactory _gameFactory;

        public event Action<IDroppable> Destroyed;

        public DestroyDroppableReporter(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;

            _gameFactory.DroppableCreated += OnDroppableCreated;
        }

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