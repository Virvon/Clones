using Clones.StaticData;
using Clones.Types;

namespace Clones.Services
{
    public interface IMainMenuStaticDataService : IStaticDataService
    {
        MainMenuStaticData GetMainMenu();
        CloneStaticData GetClone(CloneType type);
        WandStaticData GetWand(WandType type);
    }
}