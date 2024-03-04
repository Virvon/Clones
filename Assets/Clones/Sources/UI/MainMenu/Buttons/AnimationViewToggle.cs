using UnityEngine;

namespace Clones.UI
{
    public class AnimationViewToggle : MonoBehaviour
    {
        [SerializeField] private AnimationView _animationView;

        public void Open()
        {
            _animationView.gameObject.SetActive(true);
            _animationView.Open();
        }

        public void Close() =>
            _animationView.Close(() => _animationView.gameObject.SetActive(false));
    }
}