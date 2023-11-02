using Clones.Types;

namespace Clones.GameLogic
{
    public interface ICurrentBiome : IDisable
    {
        BiomeType Type { get; }
    }
}