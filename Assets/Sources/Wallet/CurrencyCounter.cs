using System;
using UnityEngine;
using Clones.Progression;
using Clones.Data;

public class CurrencyCounter : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private CurrecncyCounterData _currecncyCounterData;

    [SerializeField] private ComplexityCounter _waveCounter;
    [SerializeField] private ComplexityCounter _questCounter;

    private TargetVisitor _visitor;

    public event Action<PreyResourceType, int> MiningFacilityBroked;
    public event Action<int> DNATaked;

    private void Start()
    {
        DNATaked += OnDNATaked;

        _visitor = new TargetVisitor(_currecncyCounterData, _waveCounter, _questCounter, MiningFacilityBroked: MiningFacilityBroked, DNATaked: DNATaked);
    }

    private void OnDisable() => DNATaked -= OnDNATaked;

    public void OnKill(IRewardle visitoreble) => visitoreble.Accept(_visitor);

    private void OnDNATaked(int count) => _wallet.TakeDNA(count);

    private class TargetVisitor : IVisitor
    {
        private CurrecncyCounterData _currecncyCounterData;
        private RewardProgression _rewardProgression;
        private DropProgression _dropProgression;

        ComplexityCounter _waveCounter;
        ComplexityCounter _questCounter;

        private event Action<PreyResourceType, int> s_MiningFacilityBroked;
        private event Action<int> s_DNATaked;

        public TargetVisitor(CurrecncyCounterData currecncyCounterData, ComplexityCounter waveCounter, ComplexityCounter questCounter, Action<PreyResourceType, int> MiningFacilityBroked = null, Action<int> DNATaked = null)
        {
            _currecncyCounterData = currecncyCounterData;
            _rewardProgression = new RewardProgression();
            _dropProgression = new DropProgression();
            _waveCounter = waveCounter;
            _questCounter = questCounter;
            s_MiningFacilityBroked = MiningFacilityBroked;
            s_DNATaked = DNATaked;
        }

        public void Visit(Enemy enemy)
        {
            s_DNATaked?.Invoke(_rewardProgression.GetRewardCount(_waveCounter.complexity, _currecncyCounterData.BaseEnemyDNAReward));
        }

        public void Visit(PreyResource miningFacility)
        {
            s_DNATaked?.Invoke(_rewardProgression.GetRewardCount(_waveCounter.complexity, _currecncyCounterData.BasePreyRecourceDNAReward));
            s_MiningFacilityBroked?.Invoke(miningFacility.Type, _dropProgression.GetDropCount(_questCounter.complexity, _currecncyCounterData.BasePreyRecourceDropCount));
        }
    }
}
