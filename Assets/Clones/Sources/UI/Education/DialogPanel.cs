using Clones.Animation;
using UnityEngine;

namespace Clones.UI
{
    public class DialogPanel : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _dialogPanel;

        public void Open()
        {
            Debug.Log("open dialog panel");
            _dialogPanel.SetActive(true);
            _animator.SetBool(AnimationPath.UI.Bool.IsOpened, true);
        }

        public void Close() => 
            _animator.SetBool(AnimationPath.UI.Bool.IsOpened, false);

        public void Disable() => 
            _dialogPanel.SetActive(false);
    }
}
