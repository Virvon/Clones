using UnityEngine;

namespace Clones.Progression
{
    public class QuestProgression
    {
        public int GetItemsCount(int questLevel, int baseItemsCount)
        {
            return (int)(baseItemsCount * Mathf.Exp(questLevel / 10));
        }
    }
}
