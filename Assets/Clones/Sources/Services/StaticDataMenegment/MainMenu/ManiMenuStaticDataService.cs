using Clones.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.Services
{
    public class ManiMenuStaticDataService : IMainMenuStaticDataService
    {
        private Dictionary<CardCloneType, CardCloneStaticData> _cardClones;

        public void Load()
        {
            _cardClones = Resources.LoadAll<CardCloneStaticData>(MainMenuStaticDataPath.CardClones).ToDictionary(value => value.Type, value => value);
        }

        public MainMenuStaticData GetMainMenu() => 
            Resources.Load<MainMenuStaticData>(MainMenuStaticDataPath.MainMenu);

        public CardCloneStaticData GetCardClone(CardCloneType type) => 
            _cardClones.TryGetValue(type, out CardCloneStaticData staticData) ? staticData : null;
    }
}