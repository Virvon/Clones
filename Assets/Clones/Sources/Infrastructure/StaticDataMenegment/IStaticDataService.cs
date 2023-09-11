using Clones.Data;

namespace Clones.Infrastructure
{
    public interface IStaticDataService : IService
    {
        WorldGeneratorData GetWorldGeneratorData();
    }
}