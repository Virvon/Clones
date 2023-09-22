using Clones.Services;
using UnityEngine;

namespace Clones.UI
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject _background;

        private ITimeScale _timeScale;

        public void Init(ITimeScale timeScale) =>
            _timeScale = timeScale;

        public void Open()
        {
            _timeScale.Scaled(0);
            _background.SetActive(true);
        }

        public void Close()
        {
            _timeScale.Scaled(1);
            _background.SetActive(false);
        }
    }
}
