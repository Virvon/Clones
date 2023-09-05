public class DNAItem : Item
{
    public override void Accept(IItemVisitor visitor) => visitor.Visit(this);
}
