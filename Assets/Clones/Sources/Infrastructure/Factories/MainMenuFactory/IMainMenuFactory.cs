using Clones.Services;
using Clones.StaticData;

namespace Clones.Infrastructure
{
    public interface IMainMenuFactory : IService
    {
        void CreateMainMenu();
        void CreateCardClone(CardCloneType type);
    }
}