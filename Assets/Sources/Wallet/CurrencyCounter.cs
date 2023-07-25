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
        PreyResourceTaked += OnPreyResourceTaked;
        
        _visitor = new ItemVisitor(DNATaked: DNATaked, PreyResourceTaked: PreyResourceTaked);    
    }

    private void OnEnable()
    {
        DNATaked -= OnDNATaked;
        PreyResourceTaked -= OnPreyResourceTaked;
    }

    public void OnTakeItem(Item item)
    {
        item.Accept(_visitor);
    }

    private void OnDNATaked()
    {
        _wallet.TakeDNA(1);
    }

    private void OnPreyResourceTaked(CollectingItem collectingItem)
    {
        _quest.TakePreyResourceItem(collectingItem.Data, 1);
    }

    private class ItemVisitor : IItemVisitor
    {
        private event Action<CollectingItem> s_PreyResourceTaked;
        private event Action s_DNATaked;

        public ItemVisitor(Action<CollectingItem> PreyResourceTaked = null, Action DNATaked = null)
        {
            s_PreyResourceTaked = PreyResourceTaked;
            s_DNATaked = DNATaked;
        }

        public void Visit(DNAItem DNA)
        {
            s_DNATaked?.Invoke();
        }

        public void Visit(CollectingItem collectingItem)
        {
            s_PreyResourceTaked?.Invoke(collectingItem);
        }
    }
}
