using Clones.Infrastructure;
using Clones.StaticData;

namespace Clones.GameLogic
{
    public interface IQuestsCreator : IService
    {
        bool IsQuestItem(ItemType type);
    }
}