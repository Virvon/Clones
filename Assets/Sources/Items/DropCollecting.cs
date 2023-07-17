using System.Collections.Generic;
using UnityEngine;

public class DropCollecting : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CurrencyCounter _currecyCounter;

    private readonly Collider[] _overlapColliders = new Collider[64];
    private List<Item> _collictingItems = new List<Item>();

    private void Update()
    {
        List<Item> items = new List<Item>();

        if(TryGetNearDrop(out items) == false)
            return;

        foreach (var item in items)
            item.TakeMove(transform, 20);

        _collictingItems.AddRange(items);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Item item))
        {
            _currecyCounter.OnTakeItem(item);
            _collictingItems.Remove(item);
            Destroy(item.gameObject);
        }
    }

    private bool TryGetNearDrop(out List<Item> items)
    {
        items = new List<Item>();
        int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, _player.DropCollectingRadius, _overlapColliders);

        for(var i = 0; i < overlapCount; i++)
        {
            if (_overlapColliders[i].TryGetComponent(out Item item) && _collictingItems.Contains(item) == false)
                items.Add(item);
        }

        return items.Count > 0 ? true : false;
    } 
}
