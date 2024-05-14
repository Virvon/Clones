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
                Debug.Log("success autorize");
                Close(callback: OpenLeaderboard);

            }, onErrorCallback: (value) =>
            {
                Debug.Log("error authorize " + value);
                Close();
            });

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
