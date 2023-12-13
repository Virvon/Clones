using Clones.GameLogic;
using System.Collections.Generic;
using UnityEngine;

public class DropCollecting : MonoBehaviour
{
    private const float _speed = 25;

    private readonly Collider[] _overlapColliders = new Collider[64];

    private List<ItemMovement> _collectingItems = new();
    private float _radius;
    private IItemsCounter _itemsCounter;

    private void Update()
    {
        if(TryGetNearDrop(out List<ItemMovement> items) == false)
            return;

        foreach (var item in items)
            item.TakeMove(transform, _speed);

        _collectingItems.AddRange(items);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ItemMovement item))
        {
            _itemsCounter.TakeItem(item.GetComponent<IItem>());
            _collectingItems.Remove(item);
            Destroy(item.gameObject);
        }
    }

    public void Init(IItemsCounter itemsCounter, float radius)
    {
        _itemsCounter = itemsCounter;
        _radius = radius;
    }

    private bool TryGetNearDrop(out List<ItemMovement> items)
    {
        items = new List<ItemMovement>();
        int overlapCount = Physics.OverlapSphereNonAlloc(transform.position, _radius, _overlapColliders);

        for(var i = 0; i < overlapCount; i++)
        {
            if (_overlapColliders[i].TryGetComponent(out ItemMovement item) && _collectingItems.Contains(item) == false)
                items.Add(item);
        }

        return items.Count > 0;
    } 
}
