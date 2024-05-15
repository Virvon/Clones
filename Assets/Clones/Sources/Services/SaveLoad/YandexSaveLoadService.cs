using Agava.YandexGames.Utility;
using Clones.Data;
using Debug = UnityEngine.Debug;

namespace Clones.Services
{
    public class YandexSaveLoadService : ISaveLoadService
    {
        private const string Key = "Progress1";

        private readonly IPersistentProgressService _persistentProgress;

        public YandexSaveLoadService(IPersistentProgressService persistentProgress) => 
            _persistentProgress = persistentProgress;

        public PlayerProgress LoadProgress() =>
            PlayerPrefs.GetString(Key)?.ToDeserialized<PlayerProgress>();

        public void SaveProgress()
        {
            PlayerPrefs.SetString(Key, _persistentProgress.Progress.ToJson());
            PlayerPrefs.Save(onSuccessCallback: ()=> Debug.Log("success save prefs"), onErrorCallback: (value) => Debug.Log("error save prefs " + value));
        }
    }
}