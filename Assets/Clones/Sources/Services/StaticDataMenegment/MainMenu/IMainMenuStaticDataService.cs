using Clones.StaticData;

namespace Clones.Services
{
    public interface IMainMenuStaticDataService : IStaticDataService
    {
        MainMenuStaticData GetMainMenu();
        CardCloneStaticData GetCardClone(CardCloneType type);
        WandStaticData GetWand(WandType type);
    }
}