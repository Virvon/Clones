namespace Clones.Services
{
    public interface IItemVisitor
    {
        public void Visit(CurrencyItem currencyItem);

        public void Visit(QuestItem questItem);
    }
}
