using Clones.Data;
using UnityEngine;

namespace Clones.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string Key = "Progress";

        private readonly IPersistentProgressService _persistentProgress;

        public SaveLoadService(IPersistentProgressService persistentProgress) => 
            _persistentProgress = persistentProgress;

        public void SaveProgress() => 
            PlayerPrefs.SetString(Key, _persistentProgress.Progress.ToJson());

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(Key)?.ToDeserialized<PlayerProgress>();
    }
}