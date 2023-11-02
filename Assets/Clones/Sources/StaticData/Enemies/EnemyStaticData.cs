using Clones.Types;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Data/Create new enemy", order = 51)]
    public class EnemyStaticData : ScriptableObject
    {
        public Enemy Prefab;
        public EnemyType Type;
        public float Damage;
        public int Health;
        public float AttackCooldown;
        public float MinStopDistance;
        public float MaxStopDistance;
        public GameObject DeathEffect;
        public Vector3 EffectOffset;
    }
}
