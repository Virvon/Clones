using Clones.StaticData;

namespace Clones.GameLogic
{
    public interface ICurrentBiome : IDisable
    {
        BiomeType Type { get; }
    }
}