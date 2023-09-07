using Clones.Data;

namespace Clones.Infrastructure
{
    public interface ISavedProgress : ISaveProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}