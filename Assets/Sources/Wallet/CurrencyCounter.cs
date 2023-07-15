using System;
using UnityEngine;
using Clones.Progression;
using Clones.Data;

public class CurrencyCounter : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private CurrecncyCounterData _currecncyCounterData;

    [SerializeField] private Complexity _complexity;

    private TargetVisitor _visitor;

    public event Action<PreyResourceType, int> MiningFacilityBroked;
    public event Action<int> DNATaked;

    private void Start()
    {
        DNATaked += OnDNATaked;

        _visitor = new TargetVisitor(_currecncyCounterData, _complexity, MiningFacilityBroked: MiningFacilityBroked, DNATaked: DNATaked);
    }

    private void OnDisable() => DNATaked -= OnDNATaked;

    public void OnKill(IRewardle visitoreble) => visitoreble.Accept(_visitor);

    private void OnDNATaked(int count) => _wallet.TakeDNA(count);

    private class TargetVisitor : IVisitor
    {
        private CurrecncyCounterData _currecncyCounterData;
        private Complexity _complexity;

        private event Action<PreyResourceType, int> s_MiningFacilityBroked;
        private event Action<int> s_DNATaked;

        public TargetVisitor(CurrecncyCounterData currecncyCounterData, Complexity complexity, Action<PreyResourceType, int> MiningFacilityBroked = null, Action<int> DNATaked = null)
        {
            _currecncyCounterData = currecncyCounterData;
            _complexity = complexity;
            s_MiningFacilityBroked = MiningFacilityBroked;
            s_DNATaked = DNATaked;
        }

        public void Visit(Enemy enemy)
        {
            s_DNATaked?.Invoke((int)(_currecncyCounterData.BaseEnemyDNAReward * _complexity.ResultComplexity));
        }

        public void Visit(PreyResource preyResource)
        {
            s_DNATaked?.Invoke((int)(_currecncyCounterData.BasePreyRecourceDNAReward * _complexity.ResultComplexity));
            s_MiningFacilityBroked?.Invoke(preyResource.Type, (int)(_currecncyCounterData.BasePreyRecourceDropCount * _complexity.ResultComplexity));
        }
    }
}
