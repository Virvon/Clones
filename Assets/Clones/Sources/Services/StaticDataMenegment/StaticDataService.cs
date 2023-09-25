using Clones.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.Services
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<BiomeType, BiomeStaticData> _biomes;
        private Dictionary<PreyResourceType, PreyResourceStaticData> _preyResources;
        private Dictionary<QuestItemType, QuestItemStaticData> _questItems;
        private Dictionary<CurrencyItemType, CurrencyItemStaticData> _currencyItems;
        private Dictionary<EnemyType, EnemyStaticData> _enemies;
        private Dictionary<BoostType, BoostStaticData> _boosts;

        public void Load()
        {
            _biomes = Resources.LoadAll<BiomeStaticData>(StaticDataPath.Biomes).ToDictionary(value => value.Type, value => value);
            _preyResources = Resources.LoadAll<PreyResourceStaticData>(StaticDataPath.PreyResources).ToDictionary(value => value.Type, value => value);
            _questItems = Resources.LoadAll<QuestItemStaticData>(StaticDataPath.QuestItems).ToDictionary(value => value.Type, value => value);
            _currencyItems = Resources.LoadAll<CurrencyItemStaticData>(StaticDataPath.CurrencyItems).ToDictionary(value => value.Type, value => value);
            _enemies = Resources.LoadAll<EnemyStaticData>(StaticDataPath.Enemies).ToDictionary(value => value.Type, value => value);
            _boosts = Resources.LoadAll<BoostStaticData>(StaticDataPath.Boosts).ToDictionary(value => value.Type, value => value);
        }

        public WorldGeneratorStaticData GetWorldGeneratorData() =>
            Resources.Load<WorldGeneratorStaticData>(StaticDataPath.WorldGenerator);

        public BoostsSpawnerStaticData GetBoostsSpawnerStaticData() => 
            Resources.Load<BoostsSpawnerStaticData>(StaticDataPath.BoostsGenerator);

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

        public BoostStaticData GetBoostStaticData(BoostType type) => 
            _boosts.TryGetValue(type, out BoostStaticData staticData) ? staticData : null;
    }
}