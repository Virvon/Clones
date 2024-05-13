namespace Clones.GameLogic
{
    public interface IMainScoreCounter : IScoreCounter
    {
        void Add(IScoreCounter scoreCounter);
        void Clear();
    }
}
