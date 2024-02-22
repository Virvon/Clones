using Clones.Animation;
using UnityEngine;

namespace Clones.UI
{
    public class DialogPanel : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void Open()
        {
            gameObject.SetActive(true);
            _animator.SetBool(AnimationPath.UI.Bool.IsOpened, true);
        }

        public void Close()
        {
            _animator.SetBool(AnimationPath.UI.Bool.IsOpened, false);
        }
    }
}
