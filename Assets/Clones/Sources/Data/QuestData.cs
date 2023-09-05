using System.Collections.Generic;
using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Data/Create new quest", order = 51)]
    public class QuestData : ScriptableObject
    {
        [SerializeField] private int _itemsCount;
        [SerializeField] private int _reward;
        [SerializeField, Range(0, 100)] private float _minimumPercentageItemCountInQuest;
        [SerializeField] private List<ItemData> _questItemDatas;

        public int ItemsCount => _itemsCount;
        public int Reward => _reward;
        public float MinimumPercentageItemCountInQuest => _minimumPercentageItemCountInQuest / 100;
        public List<ItemData> QuestItemDatas => _questItemDatas;
    }
}
