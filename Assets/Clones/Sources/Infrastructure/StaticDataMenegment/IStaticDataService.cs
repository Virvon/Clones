using Clones.StaticData;

namespace Clones.Infrastructure
{
    public interface IStaticDataService : IService
    {
        void Load();
        BiomeStaticData GetBiomeStaticData(BiomeType type);
        PreyResourceStaticData GetPreyResourceStaticData(PreyResourceType type);
        WorldGeneratorStaticData GetWorldGeneratorData();
    }
}