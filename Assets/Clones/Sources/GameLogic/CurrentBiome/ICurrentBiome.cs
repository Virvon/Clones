using Clones.Types;

namespace Clones.GameLogic
{
    public interface ICurrentBiome : IDisabled
    {
        BiomeType Type { get; }
    }
}