using Clones.Animation;
using UnityEngine;

namespace Clones.UI
{
    [RequireComponent(typeof(Animator))]
    public class GameOverView : MonoBehaviour
    {
        private Animator _animator;

        private void Start() => _animator = GetComponent<Animator>();

        private void OnDisable()
        {
            Time.timeScale = 1;
        }

        public void Open()
        {
            _animator.SetBool(Animations.UI.Bools.IsOpened, true);
            Time.timeScale = 0;
        }

        public void Close()
        {
            _animator.SetBool(Animations.UI.Bools.IsOpened, false);
            Time.timeScale = 1;
        }
    }
}
