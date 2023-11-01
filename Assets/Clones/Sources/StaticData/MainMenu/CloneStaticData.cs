using Clones.UI;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New CardClone", menuName = "Data/Create new card clone", order = 51)]
    public class CloneStaticData : ScriptableObject
    {
        public GameObject Prefab;
        public CloneCard Card;
        public CloneType Type;
        public int Helath;
        public int IncreaseHealth;
        public int Damage;
        public int IncreaseDamage;
        public int UpgradePrice;
        public int IncreasePrice;
        public int DisuseTime;
        public float MovementSpeed;
        public float RotationSpeed;
        public int BuyPrice;
        public bool IsBuyed;
    }
}