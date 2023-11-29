using Clones.Types;
using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Data/Create new quest", order = 51)]
    public class QuestStaticData : ScriptableObject
    {
        //[SerializeField] private int _itemsCount;
        //[SerializeField] private int _reward;
        //[SerializeField, Range(0, 100)] private float _minimumPercentageItemCountInQuest;

        public QuestItemType[] QuestItemTypes;
    }
}
