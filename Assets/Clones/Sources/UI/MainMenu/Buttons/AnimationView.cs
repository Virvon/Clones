using System;
using Clones.Animation;
using UnityEngine;

namespace Clones.UI
{
    public class AnimationView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private Action _callback;

        public void Open() => 
            _animator.SetBool(AnimationPath.UI.Bool.IsOpened, true);

        public void Close(Action callback = null)
        {
            _callback = callback;
            _animator.SetBool(AnimationPath.UI.Bool.IsOpened, false);
        }

        public void OnClosed() => 
            _callback?.Invoke();
    }
}