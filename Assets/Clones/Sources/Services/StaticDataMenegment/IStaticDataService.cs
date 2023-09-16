using Clones.StaticData;

namespace Clones.Services
{
    public interface IStaticDataService : IService
    {
        void Load();
        WorldGeneratorStaticData GetWorldGeneratorData();
        BiomeStaticData GetBiomeStaticData(BiomeType type);
        PreyResourceStaticData GetPreyResourceStaticData(PreyResourceType type);
        ItemStaticData GetItemStaticData(ItemType type);
    }
}