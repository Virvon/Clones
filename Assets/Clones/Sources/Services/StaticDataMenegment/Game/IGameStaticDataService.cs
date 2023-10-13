using Clones.StaticData;

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
    }
}