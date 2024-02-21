using Clones.Data;
using Clones.StaticData;
using Clones.Types;

namespace Clones.Services
{
    public interface IGameStaticDataService : IStaticDataService
    {
        WorldGeneratorStaticData GetWorldGenerator();
        BiomeStaticData GetBiome(BiomeType type);
        PreyResourceStaticData GetPreyResource(PreyResourceType type);
        QuestItemStaticData GetItem(QuestItemType type);
        CurrencyItemStaticData GetItem(CurrencyItemType type);
        EnemyStaticData GetEnemy(EnemyType type);
        BulletStaticData GetBullet(BulletType type);
        EnemiesSpawnerStaticData GetEnemiesSpawner();
        QuestStaticData GetQuest();
        UnminedPreyResourceStaticData GetUnminedResource(UnminedResourceType type);
        ComplexityStaticData GetComplextiy();
        EducationPreyResourcesSpawnerStaticData GetEducationPreyResourcesSpawner();
        ItemsCounterStaticData GetItemsCounter();
        EducationQuestStaticData GetEducationQuest();
    }
}