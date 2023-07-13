using UnityEngine;

namespace Clones.Progression
{
    public class RewardProgression
    {
        public int GetRewardCount(int comcomplexity, float baseCharacteristic)
        {
            return (int)(baseCharacteristic * Mathf.Exp(comcomplexity / 10f));
        }
    }
}
