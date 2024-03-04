using Clones.Animation;
using System;
using UnityEngine;

namespace Clones.UI
{
    public class RevivalViewAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private Action _openCallback;
        private Action _closeCallback;

        public void Open(Action callback = null)
        {
            _openCallback = callback;
            _animator.SetBool(AnimationPath.UI.Bool.IsOpened, true);
        }

        public void Close(Action callback = null)
        {
            _closeCallback = callback;
            _animator.SetBool(AnimationPath.UI.Bool.IsOpened, false);
        }

        public void OnOpened() => 
            _openCallback?.Invoke();

        public void OnClosed() => 
            _closeCallback?.Invoke();
    }
}
