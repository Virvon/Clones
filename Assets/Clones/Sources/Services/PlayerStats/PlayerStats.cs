using UnityEngine;

namespace Clones.Services
{
    public class PlayerStats : IPlayerStats
    {
        public GameObject Prefab { get; private set; }
        public int _health { get; private set; }
        public int _damage { get; private set; }
        public float _attackCooldown { get; private set; }
        public float _resourceMultiplier { get; private set; }

        public void Set(GameObject prefab, int health, int damage, float attackCooldown, float resourceMultiplier)
        {
            Prefab = prefab;
            _health = health;
            _damage = damage;
            _attackCooldown = attackCooldown;
            _resourceMultiplier = resourceMultiplier;
        }
    }
}
