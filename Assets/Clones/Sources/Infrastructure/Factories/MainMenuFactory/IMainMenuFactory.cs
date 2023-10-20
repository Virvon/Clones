using Clones.Services;
using Clones.StaticData;
using UnityEngine;

namespace Clones.Infrastructure
{
    public interface IMainMenuFactory : IService
    {
        GameObject CreateMainMenu();
        void CreateCloneCard(CloneType type);
        void CreateWandCard(WandType type);
        ClonesCardsView CreateClonesCardsView();
        WandsCardsView CreateWandsCardsView();
        void CreatePlayButton();
        void CreateShowCardButtonds();
        void CreateStatsView();
        void CreateUpgradeView();
    }
}