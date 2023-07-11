using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Data/Create new quest", order = 51)]
    public class QuestData : ScriptableObject
    {
        [SerializeField] private int _baseItemsCount;
        [SerializeField, Range(0, 100)] private float _minimumPercentageItemCountInQuest;

        public int BaseItemsCount => _baseItemsCount;
        public float MinimumPercentageItemCountInQuest => _minimumPercentageItemCountInQuest / 100;
    }
}
