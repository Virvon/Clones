using UnityEngine;

namespace Clones.StaticData
{
    [CreateAssetMenu(fileName = "New CurrencyCounter", menuName = "Data/Create new currency counter", order = 51)]
    public class CurrecncyCounterData : ScriptableObject
    {
        [SerializeField] private float _dnaReward;
        [SerializeField] private float _collectingItemReward;

        public float DNAReward => _dnaReward;
        public float CollectingItemReward => _collectingItemReward;
    }
}
