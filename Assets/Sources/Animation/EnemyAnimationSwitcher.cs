using UnityEngine;

namespace Clones.Animation
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationSwitcher : MonoBehaviour
    {
        [SerializeField] private CharacterAttack _characterAttack;

        private Animator _animator;

        private void OnEnable()
        {
            _animator = GetComponent<Animator>();

            _characterAttack.AttackStarted += OnAttackStarted;
        }

        private void OnDisable() => _characterAttack.AttackStarted -= OnAttackStarted;

        public void SetMovement(bool isMoved) => _animator.SetBool(Animations.Enemy.Bools.IsMoved, isMoved);

        private void OnAttackStarted() => _animator.SetTrigger(Animations.Enemy.Triggers.Attack);
    }
}
