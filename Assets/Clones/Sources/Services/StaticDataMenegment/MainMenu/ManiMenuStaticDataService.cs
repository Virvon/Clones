using Clones.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.Services
{
    public class ManiMenuStaticDataService : IMainMenuStaticDataService
    {
        private Dictionary<CardCloneType, CardCloneStaticData> _cardClones;
        private Dictionary<WandType, WandStaticData> _wands;

        public void Load()
        {
            _cardClones = Resources.LoadAll<CardCloneStaticData>(MainMenuStaticDataPath.Clones).ToDictionary(value => value.Type, value => value);
            _wands = Resources.LoadAll<WandStaticData>(MainMenuStaticDataPath.Wands).ToDictionary(value => value.Type, value => value);
        }

        public MainMenuStaticData GetMainMenu() => 
            Resources.Load<MainMenuStaticData>(MainMenuStaticDataPath.MainMenu);

        public CardCloneStaticData GetCardClone(CardCloneType type) => 
            _cardClones.TryGetValue(type, out CardCloneStaticData staticData) ? staticData : null;

        public WandStaticData GetWand(WandType type) => 
            _wands.TryGetValue(type, out WandStaticData staticData) ? staticData : null;
    }
}