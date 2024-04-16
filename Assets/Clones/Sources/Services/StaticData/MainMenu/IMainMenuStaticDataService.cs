using Clones.StaticData;
using Clones.Types;
using UnityEngine;

namespace Clones.Services
{
    public interface IMainMenuStaticDataService : IStaticDataService
    {
        MainMenuStaticData GetMainMenu();
        CloneStaticData GetClone(CloneType type);
        WandStaticData GetWand(WandType type);
    }
}