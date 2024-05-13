using UnityEngine;

namespace Clones.UI
{
    public class ViewToggle : MonoBehaviour
    {
        [SerializeField] private GameObject _view;

        public void Open() => 
            _view.gameObject.SetActive(true);

        public void Close() =>
            _view.gameObject.SetActive(false);
    }
}