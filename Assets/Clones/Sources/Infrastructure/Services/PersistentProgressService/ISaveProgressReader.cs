using Clones.Data;

namespace Clones.Infrastructure
{
    public interface ISaveProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}