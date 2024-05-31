using Clones.Character.Attack;
using UnityEngine;

namespace Clones.Animation
{
    public class EnemyAnimationSwitcher : MonoBehaviour
    {
        [SerializeField] private CharacterAttack _characterAttack;
        [SerializeField] private Animator _animator;

        private void OnEnable() => 
            _characterAttack.AttackStarted += OnAttackStarted;

        private void OnDisable() => 
            _characterAttack.AttackStarted -= OnAttackStarted;

        public void SetMovement(bool isMoved) => 
            _animator.SetBool(AnimationPath.Enemy.Bool.IsMoved, isMoved);

        private void OnAttackStarted() =>
            _animator.SetTrigger(AnimationPath.Enemy.Trigger.Attack);
    }
}
