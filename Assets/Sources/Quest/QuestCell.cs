public class QuestCell
{
    public int MaxCount { get; private set; }
    public int CurrentCount { get; private set; }

    public bool IsFull => CurrentCount == MaxCount;

    public QuestCell(int count)
    {
        MaxCount = count;
        CurrentCount = 0;
    }

    public bool TryGetItems(int count)
    {
        //if (type != Type)
            //return false;
        if (IsFull)
            return false;
        else if (CurrentCount + count > MaxCount)
            CurrentCount = MaxCount;
        else
            CurrentCount += count;

        return true;
    }
}
