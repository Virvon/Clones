using UnityEngine;

namespace Clones.Services
{
    public interface IPlayerStats : IService
    {
        float _attackCooldown { get; }
        int _damage { get; }
        int _health { get; }
        float _resourceMultiplier { get; }
        GameObject Prefab { get; }

        void Set(GameObject prefab, int health, int damage, float attackCooldown, float resourceMultiplier);
    }
}