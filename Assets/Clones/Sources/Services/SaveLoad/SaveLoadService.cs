using Clones.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.Services
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string Key = "Progress";

        private readonly IPersistentProgressService _persistentProgress;

        private List<ISaveProgressReader> _progressReaders;
        private List<ISavedProgress> _progressWriters;

        public IReadOnlyList<ISaveProgressReader> ProgressReaders => _progressReaders;
        public IReadOnlyList<ISavedProgress> ProgressWriters => _progressWriters;

        public SaveLoadService(IPersistentProgressService persistentProgress)
        {
            _persistentProgress = persistentProgress;

            _progressReaders = new List<ISaveProgressReader>();
            _progressWriters = new List<ISavedProgress>();
        }

        public void Register(ISaveProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                _progressWriters.Add(progressWriter);

            _progressReaders.Add(progressReader);
        }

        public void Cleanup()
        {
            _progressReaders.Clear();
            _progressWriters.Clear();
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressWriter in _progressWriters)
                progressWriter.UpdateProgress(_persistentProgress.Progress);

            PlayerPrefs.SetString(Key, _persistentProgress.Progress.ToJson());
        }

        public PlayerProgress LoadProgress() => 
            PlayerPrefs.GetString(Key)?.ToDeserialized<PlayerProgress>();
    }
}