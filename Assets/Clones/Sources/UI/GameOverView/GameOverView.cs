using UnityEngine;

namespace Clones.UI
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject _background;

        public void Open()
        {
            _background.SetActive(true);
            Time.timeScale = 0;
        }

        public void Close()
        {
            _background.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
