using UnityEngine;

namespace Clones.Progression
{
    public class DropProgression
    {
        public int GetDropCount(int questLevev, int baseDropCount)
        {
            return (int)(baseDropCount * Mathf.Exp(questLevev / 20f));
        }
    }
}
