using Clones.Biomes;
using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Data/Create new enemy", order = 51)]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private int _baseDamage;
        [SerializeField] private int _baseHealth;
        [SerializeField] private float _baseAttackSpeed;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private BiomeType _biomeType;

        public Enemy EnemyPrefab => _enemyPrefab;
        public BiomeType BiomeType => _biomeType;

        public Stats GetStats() => new Stats(_baseHealth, _baseDamage, _baseAttackSpeed);
    }
}
