using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Wand", menuName = "Data/Create new wand", order = 51)]
    public class WandStaticDat : ScriptableObject
    {
        public GameObject Prefab;
        public WandType Type;
        public int Damage;
        public float Cooldown;
    }
}
