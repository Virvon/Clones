using Clones.GameLogic;

public interface IItem
{
    void Accept(IItemVisitor visitor);
}
