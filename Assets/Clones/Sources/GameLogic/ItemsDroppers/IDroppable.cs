using System;

namespace Clones.GameLogic
{
    public interface IDroppable : IDamageable
    {
        void Accept(IDroppableVisitor visitor);
    }
}