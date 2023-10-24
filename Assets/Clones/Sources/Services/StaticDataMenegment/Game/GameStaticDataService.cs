using Clones.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.Services
{
    public class GameStaticDataService : IGameStaticDataService
    {
        private Dictionary<BiomeType, BiomeStaticData> _biomes;
        private Dictionary<PreyResourceType, PreyResourceStaticData> _preyResources;
        private Dictionary<QuestItemType, QuestItemStaticData> _questItems;
        private Dictionary<CurrencyItemType, CurrencyItemStaticData> _currencyItems;
        private Dictionary<EnemyType, EnemyStaticData> _enemies;

        public void Load()
        {
            _biomes = Resources.LoadAll<BiomeStaticData>(GameStaticDataPath.Biomes).ToDictionary(value => value.Type, value => value);
            _preyResources = Resources.LoadAll<PreyResourceStaticData>(GameStaticDataPath.PreyResources).ToDictionary(value => value.Type, value => value);
            _questItems = Resources.LoadAll<QuestItemStaticData>(GameStaticDataPath.QuestItems).ToDictionary(value => value.Type, value => value);
            _currencyItems = Resources.LoadAll<CurrencyItemStaticData>(GameStaticDataPath.CurrencyItems).ToDictionary(value => value.Type, value => value);
            _enemies = Resources.LoadAll<EnemyStaticData>(GameStaticDataPath.Enemies).ToDictionary(value => value.Type, value => value);
        }

        public WorldGeneratorStaticData GetWorldGeneratorData() =>
            Resources.Load<WorldGeneratorStaticData>(GameStaticDataPath.WorldGenerator);


        public BiomeStaticData GetBiomeStaticData(BiomeType type) => 
            _biomes.TryGetValue(type, out BiomeStaticData staticData) ? staticData : null;

        public PreyResourceStaticData GetPreyResourceStaticData(PreyResourceType type) => 
            _preyResources.TryGetValue(type, out PreyResourceStaticData staticData) ? staticData : null;

        public QuestItemStaticData GetItemStaticData(QuestItemType type) => 
            _questItems.TryGetValue(type, out QuestItemStaticData staticData) ? staticData : null;

        public CurrencyItemStaticData GetItemStaticData(CurrencyItemType type) => 
            _currencyItems.TryGetValue(type, out CurrencyItemStaticData staticData) ? staticData : null;

        public EnemyStaticData GetEnemyStaticData(EnemyType type) => 
            _enemies.TryGetValue(type, out EnemyStaticData staticData) ? staticData : null;
    }
}