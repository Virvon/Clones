using Clones.Services;
using System.Collections.Generic;
using UnityEngine;

public class DropCollecting : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private float _speed = 20;

    private readonly Collider[] _overlapColliders = new Collider[64];

    private List<ItemMovement> _collectingItems = new();
    private IItemsCounter _itemsCounter;

    private void Update()
    {
        List<ItemMovement> items;

        if(TryGetNearDrop(out items) == false)
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

    public void Init(IItemsCounter itemsCounter) =>
        _itemsCounter = itemsCounter;

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
