using System.Collections.Generic;
using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Data/Create new quest", order = 51)]
    public class QuestData : ScriptableObject
    {
        [SerializeField] private int _baseItemsCount;
        [SerializeField] private int _baseReward;
        [SerializeField, Range(0, 100)] private float _minimumPercentageItemCountInQuest;
        [SerializeField] private List<ItemData> _questItemDatas;

        public int BaseItemsCount => _baseItemsCount;
        public int BaseReward => _baseReward;
        public float MinimumPercentageItemCountInQuest => _minimumPercentageItemCountInQuest / 100;
        public List<ItemData> QuestItemDatas => _questItemDatas;
    }
}
