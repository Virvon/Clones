using Agava.YandexGames;
using Clones.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.UI
{
    public class AuthorizeView : MonoBehaviour
    {
        [SerializeField] private LeaderboardView _leaderboard;
        [SerializeField] private AnimationView _animationView;

        private IPersistentProgressService _persistentProgress;
        private ISaveLoadService _saveLoadService;
        private IProgressReadersReporter _progressReadersReporter;

        public void Init(IPersistentProgressService persistentProgress, ISaveLoadService saveLoadService, IProgressReadersReporter progressReadersReporter)
        {
            _persistentProgress = persistentProgress;
            _saveLoadService = saveLoadService;
            _progressReadersReporter = progressReadersReporter;
        }

        public void AuthorizeAccount()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.Authorize(onSuccessCallback: () =>
            {
                Agava.YandexGames.Utility.PlayerPrefs.Load(onSuccessCallback: () =>
                {
                    _persistentProgress.Progress = _saveLoadService.LoadProgress();
                    _progressReadersReporter.Report();

                    Close(callback: () =>
                    {
                        gameObject.SetActive(false);
                        OpenLeaderboard();
                    });
                });
            }, onErrorCallback: (value) => Close(callback: () => gameObject.SetActive(false)));

            if (PlayerAccount.IsAuthorized)
                PlayerAccount.RequestPersonalProfileDataPermission();
#endif
        }

        private void Close(Action callback = null) =>
            _animationView.Close(callback);

        private void OpenLeaderboard() => 
            _leaderboard.Open();
    }
}
