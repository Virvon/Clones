using Agava.YandexGames.Utility;
using Clones.Data;

namespace Clones.Services
{
    public class YandexSaveLoadService : ISaveLoadService
    {
        private const string Key = "Progress";

        private readonly IPersistentProgressService _persistentProgress;

        public YandexSaveLoadService(IPersistentProgressService persistentProgress) => 
            _persistentProgress = persistentProgress;

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(Key)?.ToDeserialized<PlayerProgress>();

        public void SaveProgress()
        {
            PlayerPrefs.SetString(Key, _persistentProgress.Progress.ToJson());
        }
    }
}