using Clones.Types;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Biome", menuName = "Data/Create new biome", order = 51)]
    public class BiomeStaticData : ScriptableObject
    {
        public BiomeType Type;
        public GameObject[] Prefabs;
        [Range(0, 100)] public int PreyResourcesPercentageFilled;
        public PreyResourceType[] PreyResourcesTypes;
        [Range(0, 100)] public int UnminedResourcesPercentageFilled;
        public UnminedResourceType[] UnminedResourcesTypes;
        public EnemyType[] EnemiesTemplated;
        public AudioSource CombatAudioSourcePrefab;

        public GameObject GetPrefab() => 
            Prefabs[Random.Range(0, Prefabs.Length)];
    }
}
