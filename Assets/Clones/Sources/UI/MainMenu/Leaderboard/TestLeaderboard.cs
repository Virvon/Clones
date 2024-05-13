using Clones.Services;
using UnityEngine;

namespace Clones.UI
{
    public class TestLeaderboard : MonoBehaviour
    {
        private int _score = 10;

        public void Fill()
        {
            AllServices.Instance.Single<ILeaderboard>().Fill();
        }

        public void Score()
        {
            AllServices.Instance.Single<ILeaderboard>().SetPlayerScore(_score);
            _score += 10;
        }
    }
}
