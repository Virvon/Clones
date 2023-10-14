using Clones.StaticData;

namespace Clones.Services
{
    public interface IMainMenuStaticDataService : IStaticDataService
    {
        MainMenuStaticData GetMainMenu();
        CloneStaticData GetCardClone(CloneType type);
        WandStaticData GetWand(WandType type);
    }
}