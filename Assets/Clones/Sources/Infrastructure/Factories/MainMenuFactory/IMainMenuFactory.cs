using Clones.Services;
using UnityEngine;
using Clones.UI;
using Clones.Types;

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
        LeaderboardElement CreateLeaderboardElement(LeaderboardPlayer player, Transform parent);
        void CreateCloneModelPoint(ICharacterFactory characterFactory);
    }
}