using UnityEngine;

public class CollectingItem : Item
{
    public override void Accept(IItemVisitor visitor) => visitor.Visit(this);
}
