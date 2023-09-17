namespace Clones.Services
{
    public interface IItemsCounter : IService
    {
        void TakeItem(IItem item);
    }
}