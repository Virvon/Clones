using Clones.Data;

namespace Clones.Infrastructure
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress Progress { get; set; }
    }
}