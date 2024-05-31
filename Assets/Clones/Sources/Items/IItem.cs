using Clones.GameLogic;

namespace Clones.Items
{
    public interface IItem
    {
        void Accept(IItemVisitor visitor);
    }
}