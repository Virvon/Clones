using Clones.Services;

public interface IItem
{
    void Accept(IItemVisitor visitor);
}
