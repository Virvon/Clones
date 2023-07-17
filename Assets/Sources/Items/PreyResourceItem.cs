using UnityEngine;

public class PreyResourceItem : Item
{
    [SerializeField] private PreyResourceType _type;

    public PreyResourceType Type => _type;

    public override void Accept(IItemVisitor visitor) => visitor.Visit(this);
}
