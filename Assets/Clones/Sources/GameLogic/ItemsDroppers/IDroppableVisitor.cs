namespace Clones.GameLogic
{
    public interface IDroppableVisitor
    {
        public void Visit(Enemy enemy);
        public void Visit(PreyResource miningFacility);
    }
}
