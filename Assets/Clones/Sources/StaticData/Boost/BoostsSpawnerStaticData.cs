using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New BoostsSpawner", menuName = "Data/Create new boosts spawner", order = 51)]
    public class BoostsSpawnerStaticData : ScriptableObject
    {
        public GameObject Prefab;
        public float MinRadius;
        public float MaxRadius;
        public float Cooldown;
        public BoostType[] SpawnedBoosts;
    }
}
