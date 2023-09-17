namespace Clones.GameLogic
{
    public interface IDroppable : IDamageable
    {
        public abstract void Accept(IDroppableVisitor visitor);
    }

}