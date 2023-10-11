using Clones.Data;

namespace Clones.Services
{
    public interface ISavedProgress : ISaveProgressReader
    {
        void UpdateProgress(PlayerProgress progress);
    }
}