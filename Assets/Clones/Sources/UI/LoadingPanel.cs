using UnityEngine;

namespace Clones.UI
{
    public class LoadingPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _background;

        private void Start()
        {
            Close();
        }

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
