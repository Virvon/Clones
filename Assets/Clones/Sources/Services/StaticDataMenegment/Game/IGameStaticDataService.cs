using Clones.Data;
using Clones.StaticData;
using Clones.Types;

namespace Clones.Services
{
    public interface IGameStaticDataService : IStaticDataService
    {
        WorldGeneratorStaticData GetWorldGeneratorData();
        BiomeStaticData GetBiomeStaticData(BiomeType type);
        PreyResourceStaticData GetPreyResourceStaticData(PreyResourceType type);
        QuestItemStaticData GetItemStaticData(QuestItemType type);
        CurrencyItemStaticData GetItemStaticData(CurrencyItemType type);
        EnemyStaticData GetEnemyStaticData(EnemyType type);
        BulletStaticData GetBullet(BulletType type);
        EnemiesSpawnerStaticData GetEnemiesSpawner();
    }
}