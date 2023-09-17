using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Data/Create new enemy", order = 51)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private int _health;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private Enemy _enemyPrefab;

        public Enemy EnemyPrefab => _enemyPrefab;
        public Stats GetStats() => new Stats(_health, _damage, _attackSpeed);
    }
}
