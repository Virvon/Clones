using Agava.YandexGames;
using UnityEngine;

namespace Clones.UI
{
    public class AuthorizeView : MonoBehaviour
    {
        [SerializeField] private LeaderboardView _leaderboard;

        public void AuthorizeAccount()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
               PlayerAccount.Authorize(onSuccessCallback: () =>
            {
                Debug.Log("success autorize");
                Close();
                OpenLeaderboard();

            }, onErrorCallback: (value) =>
            {
                Debug.Log("error authorize " + value);
                Close();
            });

            if (PlayerAccount.IsAuthorized)
                PlayerAccount.RequestPersonalProfileDataPermission(); 
#endif
        }

        public void Close() => 
            gameObject.SetActive(false);

        private void OpenLeaderboard() => 
            _leaderboard.Open();
    }
}
