using UnityEngine;

namespace Clones.Data
{
    [CreateAssetMenu(fileName = "New CurrencyCounter", menuName = "Data/Create new currency counter", order = 51)]
    public class CurrecncyCounterData : ScriptableObject
    {
        [SerializeField] private float _baseEnemyDNAReward;

        [SerializeField] private float _basePreyRecourceDNAReward;
        [SerializeField] private int _basePreyRecourceDropCount;

        public float BaseEnemyDNAReward => _baseEnemyDNAReward;

        public float BasePreyRecourceDNAReward => _basePreyRecourceDNAReward;
        public int BasePreyRecourceDropCount => _basePreyRecourceDropCount;
    }
}
