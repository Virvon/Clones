using Clones.Types;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Data/Create new quest", order = 51)]
    public class QuestStaticData : ScriptableObject
    {
        public QuestItemType[] QuestItemTypes;
        [Range(0, 100)] public int MinItemsCountPercentInQuest;
        public int ItemsCount;
        public int Reward;
    }
}