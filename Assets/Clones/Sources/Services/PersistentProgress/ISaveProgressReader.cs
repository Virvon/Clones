using Clones.Data;

namespace Clones.Services
{
    public interface ISaveProgressReader
    {
        void LoadProgress(PlayerProgress progress);
    }
}