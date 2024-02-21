using Clones.Data;
using Clones.StaticData;
using Clones.Types;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Clones.Services
{
    public class GameStaticDataService : IGameStaticDataService
    {
        private Dictionary<BiomeType, BiomeStaticData> _biomes;
        private Dictionary<PreyResourceType, PreyResourceStaticData> _preyResources;
        private Dictionary<UnminedResourceType, UnminedPreyResourceStaticData> _unminedResource;
        private Dictionary<QuestItemType, QuestItemStaticData> _questItems;
        private Dictionary<CurrencyItemType, CurrencyItemStaticData> _currencyItems;
        private Dictionary<EnemyType, EnemyStaticData> _enemies;
        private Dictionary<BulletType, BulletStaticData> _bullets;
        private EnemiesSpawnerStaticData _enemiesSpawner;
        private WorldGeneratorStaticData _worldGenerator;
        private QuestStaticData _quest;
        private ComplexityStaticData _complexity;
        private ItemsCounterStaticData _itemsCounter;
        private EducationQuestStaticData _educationQuest;
        private EducationPreyResourcesSpawnerStaticData _educationPreyResourcesSpawner;

        public void Load()
        {
            _biomes = Resources.LoadAll<BiomeStaticData>(GameStaticDataPath.Biomes).ToDictionary(value => value.Type, value => value);
            _preyResources = Resources.LoadAll<PreyResourceStaticData>(GameStaticDataPath.PreyResources).ToDictionary(value => value.Type, value => value);
            _unminedResource = Resources.LoadAll<UnminedPreyResourceStaticData>(GameStaticDataPath.UnminedResources).ToDictionary(value => value.Type, value => value);
            _questItems = Resources.LoadAll<QuestItemStaticData>(GameStaticDataPath.QuestItems).ToDictionary(value => value.Type, value => value);
            _currencyItems = Resources.LoadAll<CurrencyItemStaticData>(GameStaticDataPath.CurrencyItems).ToDictionary(value => value.Type, value => value);
            _enemies = Resources.LoadAll<EnemyStaticData>(GameStaticDataPath.Enemies).ToDictionary(value => value.Type, value => value);
            _bullets = Resources.LoadAll<BulletStaticData>(GameStaticDataPath.Bullets).ToDictionary(value => value.Type, value => value);
            _enemiesSpawner = Resources.Load<EnemiesSpawnerStaticData>(GameStaticDataPath.EnemiesSpawner);
            _worldGenerator = Resources.Load<WorldGeneratorStaticData>(GameStaticDataPath.WorldGenerator);
            _quest = Resources.Load<QuestStaticData>(GameStaticDataPath.Quest);
            _complexity = Resources.Load<ComplexityStaticData>(GameStaticDataPath.Complexity);
            _itemsCounter = Resources.Load<ItemsCounterStaticData>(GameStaticDataPath.ItemsCounter);
            _educationQuest = Resources.Load<EducationQuestStaticData>(GameStaticDataPath.EducationQuest);
            _educationPreyResourcesSpawner = Resources.Load<EducationPreyResourcesSpawnerStaticData>(GameStaticDataPath.EducationPreyResourcesSpawner);
        }

        public WorldGeneratorStaticData GetWorldGenerator() =>
            _worldGenerator;

        public BiomeStaticData GetBiome(BiomeType type) => 
            _biomes.TryGetValue(type, out BiomeStaticData staticData) ? staticData : null;

        public PreyResourceStaticData GetPreyResource(PreyResourceType type) => 
            _preyResources.TryGetValue(type, out PreyResourceStaticData staticData) ? staticData : null;

        public UnminedPreyResourceStaticData GetUnminedResource(UnminedResourceType type) =>
            _unminedResource.TryGetValue(type, out UnminedPreyResourceStaticData staticData) ? staticData : null;

        public QuestItemStaticData GetItem(QuestItemType type) => 
            _questItems.TryGetValue(type, out QuestItemStaticData staticData) ? staticData : null;

        public CurrencyItemStaticData GetItem(CurrencyItemType type) => 
            _currencyItems.TryGetValue(type, out CurrencyItemStaticData staticData) ? staticData : null;

        public EnemyStaticData GetEnemy(EnemyType type) => 
            _enemies.TryGetValue(type, out EnemyStaticData staticData) ? staticData : null;

        public BulletStaticData GetBullet(BulletType type) => 
            _bullets.TryGetValue(type, out BulletStaticData staticData) ? staticData : null;

        public EnemiesSpawnerStaticData GetEnemiesSpawner() => 
            _enemiesSpawner;

        public QuestStaticData GetQuest() => 
            _quest;

        public ComplexityStaticData GetComplextiy() => 
            _complexity;

        public ItemsCounterStaticData GetItemsCounter() => 
            _itemsCounter;

        public EducationQuestStaticData GetEducationQuest() => 
            _educationQuest;

        public EducationPreyResourcesSpawnerStaticData GetEducationPreyResourcesSpawner() => 
            _educationPreyResourcesSpawner;
    }
}