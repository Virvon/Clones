using UnityEngine;

namespace Clones.UI
{
    public class FrameFocus : MonoBehaviour
    {
        [SerializeField] private GameObject _frameFocus;
        [SerializeField] private Animator _animator;

        public void Init() =>
            _frameFocus.SetActive(false);

        public void Open()
        {
            _frameFocus.SetActive(true);
            _animator.SetBool(Animation.AnimationPath.UI.Bool.IsOpened, true);
        }

        public void Close() => 
            _animator.SetBool(Animation.AnimationPath.UI.Bool.IsOpened, false);
    }
}
