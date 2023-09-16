using Clones.StaticData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.Services
{
    public class StaticDataService : IStaticDataService
    {
        private const string WorldGeneratorStaticDataPath = "StaticData/World/WorldGenerator";
        private const string BiomesStaticDataPath = "StaticData/World/Biomes";
        private const string PreyResourcesStaticDataPath = "StaticData/World/PreyRecources";
        private const string ItemsStaticDataPath = "StaticData/World/Items";

        Dictionary<BiomeType, BiomeStaticData> _biomes;
        Dictionary<PreyResourceType, PreyResourceStaticData> _preyResources;
        Dictionary<ItemType, ItemStaticData> _items;

        public void Load()
        {
            _biomes = Resources.LoadAll<BiomeStaticData>(BiomesStaticDataPath).ToDictionary(value => value.Type, value => value);
            _preyResources = Resources.LoadAll<PreyResourceStaticData>(PreyResourcesStaticDataPath).ToDictionary(value => value.Type, value => value);
            _items = Resources.LoadAll<ItemStaticData>(ItemsStaticDataPath).ToDictionary(value => value.Type, value => value);
        }

        public BiomeStaticData GetBiomeStaticData(BiomeType type) => 
            _biomes.TryGetValue(type, out BiomeStaticData staticData) ? staticData : null;

        public WorldGeneratorStaticData GetWorldGeneratorData() =>
            Resources.Load<WorldGeneratorStaticData>(WorldGeneratorStaticDataPath);

        public PreyResourceStaticData GetPreyResourceStaticData(PreyResourceType type) => 
            _preyResources.TryGetValue(type, out PreyResourceStaticData staticData) ? staticData : null;

        public ItemStaticData GetItemStaticData(ItemType type) => 
            _items.TryGetValue(type, out ItemStaticData staticData) ? staticData : null;
    }
}