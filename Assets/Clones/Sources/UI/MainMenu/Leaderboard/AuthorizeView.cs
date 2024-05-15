using Agava.YandexGames;
using System;
using UnityEngine;

namespace Clones.UI
{
    public class AuthorizeView : MonoBehaviour
    {
        [SerializeField] private LeaderboardView _leaderboard;
        [SerializeField] private AnimationView _animationView;

        public void AuthorizeAccount()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            PlayerAccount.Authorize(onSuccessCallback: () =>
            {
                Debug.Log("authorization player prefs loading");
                Agava.YandexGames.Utility.PlayerPrefs.Load(onSuccessCallback: () =>
                {
                    Debug.Log("success load prefs");
                    Clones.Services.AllServices.Instance.Single<Clones.Services.IPersistentProgressService>().Progress = Clones.Services.AllServices.Instance.Single<Clones.Services.ISaveLoadService>().LoadProgress();

                    Close(callback: () =>
                    {
                        gameObject.SetActive(false);
                        OpenLeaderboard();
                    });
                }, onErrorCallback: (value) => Debug.Log("error load prefs " + value));
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
