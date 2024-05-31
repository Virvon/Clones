using Clones.GameLogic;
using System;
using UnityEngine;

namespace Clones.Character.Enemy
{
    public class EnemyHealth : MonoBehaviour, IDroppable, IHealthChanger
    {
        public event Action HealthChanged;
        public event Action<IDamageable> Died;

        public int Health { get; private set; }
        public bool IsAlive { get; private set; }

        public void Init(int health)
        {
            Health = health;

            IsAlive = true;
        }

        public void Accept(IDroppableVisitor visitor) =>
            visitor.Visit(GetComponent<Enemy>());

        public void TakeDamage(float damage)
        {
            Health -= (int)damage;

            HealthChanged?.Invoke();

            if (Health <= 0)
            {
                IsAlive = false;
                Health = 0;
                Died?.Invoke(this);
                Destroy(gameObject);
            }
        }

        public void Disappear()
        {
            IsAlive = false;
            Destroy(gameObject);
        }
    }
}