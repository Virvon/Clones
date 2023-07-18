using UnityEngine;

public class PreyResourceItem : Item
{
    public override void Accept(IItemVisitor visitor) => visitor.Visit(this);
}
