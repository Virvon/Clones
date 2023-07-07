using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Data/Create new quest", order = 51)]
    public class QuestData : ScriptableObject
    {
        [SerializeField] private int _baseItemsCount;

        public int BaseItemsCount => _baseItemsCount;
    }
}
