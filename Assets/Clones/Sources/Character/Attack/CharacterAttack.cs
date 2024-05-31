using System; 
using UnityEngine;

namespace Clones.Character.Attack
{
    public abstract class CharacterAttack : MonoBehaviour
    {
        private float _currentCooldown;

        public event Action AttackCompleted;
        public event Action AttackStarted;

        protected IDamageable Target { get; private set; }
        protected abstract float CoolDown { get; }

        private void Update()
        {
            if (_currentCooldown > 0)
                _currentCooldown -= Time.deltaTime;
        }

        public void TryAttack(IDamageable target)
        {
            if (_currentCooldown > 0 || target.IsAlive == false)
                return;

            _currentCooldown = CoolDown;
            Target = target;

            AttackStarted?.Invoke();
        }

        protected abstract void Attack();

        private void OnAttack()
        {
            Attack();
            AttackCompleted?.Invoke();
        }
    }
}
