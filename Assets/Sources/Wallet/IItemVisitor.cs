public interface IItemVisitor
{
    public void Visit(DNAItem DNA);

    public void Visit(PreyResourceItem preyResource);
}
