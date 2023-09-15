public interface IDroppable : IDamageable
{
    public abstract void Accept(IDroppableVisitor visitor);
}
