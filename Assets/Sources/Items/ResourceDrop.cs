using UnityEngine;
using Clones.Data;
using System;
using Random = UnityEngine.Random;

public class ResourceDrop : MonoBehaviour
{
    [SerializeField] private ResourceDropData _resourceDropData;
    [SerializeField] private Quest _quest;

    [SerializeField] private ItemData _DNAData;

    private TargetVisitor _visitor;

    private event Action<PreyResource> PreyResourceBroked;
    private event Action<Vector3> DNATaked;

    private void Start()
    {
        DNATaked += OnDNATaked;
        PreyResourceBroked += OnPreyResourceBroked;

        _visitor = new TargetVisitor(PreyResourceBroked: PreyResourceBroked, DNATaked: DNATaked);
    }

    private void OnDisable()
    {
        DNATaked -= OnDNATaked;
        PreyResourceBroked -= OnPreyResourceBroked;
    }

    public void OnKill(IDropble visitoreble) => visitoreble.Accept(_visitor);

    private void OnDNATaked(Vector3 position) => Drop(_DNAData, position);

    private void OnPreyResourceBroked(PreyResource preyResource)
    {
        if (_quest.IsQuestItem(preyResource.Data.ItemData))
            Drop(preyResource.Data.ItemData, preyResource.transform.position);
    }

    private void Drop(ItemData itemData, Vector3 dropPosition)
    {
        int itemsCount = Random.Range(1, _resourceDropData.MaxItemsCount + 1);

        for(var i = 0; i < itemsCount; i++)
        {
            Item item = Instantiate(itemData.Prefab, dropPosition + itemData.DropOffset, Quaternion.identity, transform);

            item.Init(itemData);
            item.TakeMove(GetIncideCirclePosition(dropPosition + itemData.DropOffset), itemData.DropSpeed);
        }
    }

    private Vector3 GetIncideCirclePosition(Vector3 position)
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float distance = Random.Range(0, _resourceDropData.Radius);
        Vector3 targetPosition = position + new Vector3(randomDirection.x, position.y, randomDirection.y) * distance;

        return targetPosition;
    }

    private class TargetVisitor : IDropVisitor
    {
        private event Action<PreyResource> s_PreyResourceBroked;
        private event Action<Vector3> s_DNATaked;

        public TargetVisitor(Action<PreyResource> PreyResourceBroked = null, Action<Vector3> DNATaked = null)
        {
            s_PreyResourceBroked = PreyResourceBroked;
            s_DNATaked = DNATaked;
        }

        public void Visit(Enemy enemy)
        {
            s_DNATaked?.Invoke(enemy.transform.position);
        }

        public void Visit(PreyResource preyResource)
        {
            s_DNATaked?.Invoke(preyResource.transform.position);
            s_PreyResourceBroked?.Invoke(preyResource);
        }
    }
}
