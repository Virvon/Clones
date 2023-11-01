using Clones.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.Services
{
    public class MainMenuStaticDataService : IMainMenuStaticDataService
    {
        private MainMenuStaticData _mainMenu;
        private Dictionary<CloneType, CloneStaticData> _cardClones;
        private Dictionary<WandType, WandStaticData> _wands;

        public void Load()
        {
            _mainMenu = Resources.Load<MainMenuStaticData>(MainMenuStaticDataPath.MainMenu);
            _cardClones = Resources.LoadAll<CloneStaticData>(MainMenuStaticDataPath.Clones).ToDictionary(value => value.Type, value => value);
            _wands = Resources.LoadAll<WandStaticData>(MainMenuStaticDataPath.Wands).ToDictionary(value => value.Type, value => value);
        }

        public MainMenuStaticData GetMainMenu() =>
            _mainMenu;

        public CloneStaticData GetClone(CloneType type) => 
            _cardClones.TryGetValue(type, out CloneStaticData staticData) ? staticData : null;

        public WandStaticData GetWand(WandType type) => 
            _wands.TryGetValue(type, out WandStaticData staticData) ? staticData : null;
    }
}