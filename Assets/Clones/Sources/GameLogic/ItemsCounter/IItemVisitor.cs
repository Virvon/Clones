using Clones.Items;

namespace Clones.GameLogic
{
    public interface IItemVisitor
    {
        public void Visit(CurrencyItem currencyItem);
        public void Visit(QuestItem questItem);
    }
}
