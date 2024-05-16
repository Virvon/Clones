using Agava.YandexGames.Utility;
using Clones.Data;
using Clones.Infrastructure;
using Debug = UnityEngine.Debug;

namespace Clones.Services
{
    public class YandexSaveLoadService : ISaveLoadService
    {
        private const string Key = "Progress1";

        private readonly IPersistentProgressService _persistentProgress;

        public YandexSaveLoadService(IPersistentProgressService persistentProgress)
        {
            _persistentProgress = persistentProgress;
        }

        public PlayerProgress LoadProgress()
        {
            PlayerProgress playerProgress = PlayerPrefs.GetString(Key)?.ToDeserialized<PlayerProgress>();

            return playerProgress;
        }

        public void SaveProgress()
        {
            PlayerPrefs.SetString(Key, _persistentProgress.Progress.ToJson());
            PlayerPrefs.Save();
        }
    }
}