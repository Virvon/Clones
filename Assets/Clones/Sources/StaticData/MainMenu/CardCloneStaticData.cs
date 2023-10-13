using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New CardClone", menuName = "Data/Create new card clone", order = 51)]
    public class CardCloneStaticData : ScriptableObject
    {
        public GameObject Prefab;
        public CardCloneType Type;
        public int Helath;
        public int IncreaseHealth;
        public int Damage;
        public int IncreaseDamage;
        public int UpgradePrice;
        public int IncreasePrice;
    }
}
