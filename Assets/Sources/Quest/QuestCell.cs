public class QuestCell
{
    public int MaxCount { get; private set; }
    public int CurrentCount { get; private set; }
    public PreyResourceType Type { get; private set; }

    public bool IsFull => CurrentCount == MaxCount;

    public QuestCell(int count, PreyResourceType type)
    {
        MaxCount = count;
        Type = type;
        CurrentCount = 0;
    }

    public bool TryGetItems(int count, PreyResourceType type)
    {
        if (type != Type)
            return false;
        else if(CurrentCount + count > MaxCount)
            return false;

        CurrentCount += count;

        return true;
    }
}
