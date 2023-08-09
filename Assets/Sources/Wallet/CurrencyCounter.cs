using System;
using UnityEngine;
using Clones.Progression;
using Clones.Data;

public class CurrencyCounter : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private CurrecncyCounterData _currecncyCounterData;

    [SerializeField] private Complexity _complexity;
    [SerializeField] private Quest _quest;

    private ItemVisitor _visitor;

    private event Action<CollectingItem> PreyResourceTaked;
    private event Action DNATaked;

    private void Start()
    {
        DNATaked += OnDNATaked;
        PreyResourceTaked += OnCollectingItemTaked;
        
        _visitor = new ItemVisitor(DNATaked: DNATaked, CollectingTaked: PreyResourceTaked);    
    }

    private void OnEnable()
    {
        DNATaked -= OnDNATaked;
        PreyResourceTaked -= OnCollectingItemTaked;
    }

    public void OnTakeCollectingItem(Item item)
    {
        item.Accept(_visitor);
    }

    private void OnDNATaked()
    {
        _wallet.TakeDNA((int)(_currecncyCounterData.DNAReward * _complexity.Value));
    }

    private void OnCollectingItemTaked(CollectingItem collectingItem)
    {
        _quest.TakePreyResourceItem(collectingItem.Data, (int)(_currecncyCounterData.CollectingItemReward * _complexity.Value));
    }

    private class ItemVisitor : IItemVisitor
    {
        private event Action<CollectingItem> s_CollectingItemTaked;
        private event Action s_DNATaked;

        public ItemVisitor(Action<CollectingItem> CollectingTaked = null, Action DNATaked = null)
        {
            s_CollectingItemTaked = CollectingTaked;
            s_DNATaked = DNATaked;
        }

        public void Visit(DNAItem DNA)
        {
            s_DNATaked?.Invoke();
        }

        public void Visit(CollectingItem collectingItem)
        {
            s_CollectingItemTaked?.Invoke(collectingItem);
        }
    }
}
