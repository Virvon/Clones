using UnityEngine;

namespace Clones.UI
{
    public class GameOverView : MonoBehaviour
    {
        [SerializeField] private GameObject _background;

        public void Open()
        {
            _background.SetActive(true);
        }

        public void Close()
        {
            _background.SetActive(false);
        }
    }
}
