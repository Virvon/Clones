using Clones.Services;

namespace Clones.Infrastructure
{
    public interface IMainMenuFactory : IService
    {
        void CreateMainMenu();
    }
}