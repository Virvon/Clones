using Clones.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.Services
{
    public class StaticDataService : IStaticDataService
    {
        Dictionary<BiomeType, BiomeStaticData> _biomes;
        Dictionary<PreyResourceType, PreyResourceStaticData> _preyResources;
        Dictionary<QuestItemType, QuestItemStaticData> _questItems;
        Dictionary<CurrencyItemType, CurrencyItemStaticData> _currencyItems;

        public void Load()
        {
            _biomes = Resources.LoadAll<BiomeStaticData>(StaticDataPath.BiomesStaticData).ToDictionary(value => value.Type, value => value);
            _preyResources = Resources.LoadAll<PreyResourceStaticData>(StaticDataPath.PreyResourcesStaticData).ToDictionary(value => value.Type, value => value);
            _questItems = Resources.LoadAll<QuestItemStaticData>(StaticDataPath.QuestItemsStaticData).ToDictionary(value => value.Type, value => value);
            _currencyItems = Resources.LoadAll<CurrencyItemStaticData>(StaticDataPath.CurrencyItemsStaticData).ToDictionary(value => value.Type, value => value);
        }

        public BiomeStaticData GetBiomeStaticData(BiomeType type) => 
            _biomes.TryGetValue(type, out BiomeStaticData staticData) ? staticData : null;

        public WorldGeneratorStaticData GetWorldGeneratorData() =>
            Resources.Load<WorldGeneratorStaticData>(StaticDataPath.WorldGeneratorStaticData);

        public PreyResourceStaticData GetPreyResourceStaticData(PreyResourceType type) => 
            _preyResources.TryGetValue(type, out PreyResourceStaticData staticData) ? staticData : null;

        public QuestItemStaticData GetItemStaticData(QuestItemType type) => 
            _questItems.TryGetValue(type, out QuestItemStaticData staticData) ? staticData : null;

        public CurrencyItemStaticData GetItemStaticData(CurrencyItemType type) => 
            _currencyItems.TryGetValue(type, out CurrencyItemStaticData staticData) ? staticData : null;
    }
}