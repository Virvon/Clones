using Clones.Data;

namespace Clones.Infrastructure
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress Progress { get; set; }
    }
}