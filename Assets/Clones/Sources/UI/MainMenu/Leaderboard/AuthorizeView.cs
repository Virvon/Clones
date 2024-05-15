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
                Close(callback: () =>
                {
                    gameObject.SetActive(false);
                    OpenLeaderboard();
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
