using System;
using UnityEngine;

public class CurrencyCounter : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;

    private readonly TargetVisitor _visitor = new TargetVisitor();

    public event Action<MiningFacilityType> MiningFacilityBroked;

    private void OnEnable()
    {
        _visitor.DNATaked += OnDNATaked;
        _visitor.MiningFacilityBroked += OnMiningFacilityBroked;
    }

    private void OnDisable()
    {
        _visitor.MiningFacilityBroked -= OnMiningFacilityBroked;
    }

    public void OnKill(IRewardle  visitoreble) => visitoreble.Accept(_visitor);

    private void OnDNATaked(int DNACoutnt) => _wallet.TakeDNA(DNACoutnt);

    private void OnMiningFacilityBroked(MiningFacilityType type) => MiningFacilityBroked?.Invoke(type);

    private class TargetVisitor : IVisitor
    {
        public event Action<MiningFacilityType> MiningFacilityBroked;
        public event Action<int> DNATaked;

        public void Visit(Enemy enemy)
        {
            DNATaked?.Invoke(1);
        }

        public void Visit(MiningFacility miningFacility)
        {
            DNATaked?.Invoke(1);
            MiningFacilityBroked?.Invoke(miningFacility.Type);
        }
    }
}
