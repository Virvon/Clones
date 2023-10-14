using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Wand", menuName = "Data/Create new wand", order = 51)]
    public class WandStaticData : ScriptableObject
    {
        public GameObject Prefab;
        public CardWand Card;
        public WandType Type;
        public int Damage;
        public float Cooldown;
    }
}
