using UnityEngine;
using System;

namespace Clones.Character.Player
{
    public class PlayerHealth : MonoBehaviour, IDamageable, IHealthChanger
    {
        private bool _invulnerabled;

        public bool IsAlive => Health > 0;

        public int Health { get; private set; }
        public int MaxHealth { get; private set; }

        public event Action<IDamageable> Died;
        public event Action HealthChanged;
        public event Action DamageTaked;
        public event Action Reborned;

        public void Init(int health)
        {
            Health = health;
            MaxHealth = Health;
            _invulnerabled = false;
        }

        public void TakeDamage(float damage)
        {
            if (_invulnerabled)
                return;

            Health -= (int)damage;

            if (Health < 0)
                Health = 0;

            HealthChanged?.Invoke();
            DamageTaked?.Invoke();

            if (Health == 0)
            {
                Died?.Invoke(this);
                gameObject.SetActive(false);
            }
        }

        public void Reborn(int health)
        {
            gameObject.SetActive(true);
            Health = health;
            HealthChanged?.Invoke();
            Reborned?.Invoke();
        }

        public void SetInvulnerability() =>
            _invulnerabled = true;
    }
}