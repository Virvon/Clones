using Clones.Infrastructure;
using System;

namespace Clones.Services
{
    public class DestroyDroppableReporter : IDestroyDroppableReporter
    {
        public event Action<IDroppable> Destroyed;

        public void AddDroppable(IDroppable droppable) => 
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