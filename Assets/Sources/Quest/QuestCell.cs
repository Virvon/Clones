using Clones.Data;

public class QuestCell
{
    public int MaxCount { get; private set; }
    public int CurrentCount { get; private set; }
    public ItemData Type { get; private set; }

    public bool IsFull => CurrentCount == MaxCount;

    public QuestCell(ItemData item, int count)
    {
        Type = item;
        MaxCount = count;
        CurrentCount = 0;
    }

    public bool TryGetItems(ItemData type, int count)
    {
        if(type != Type)
            return false;
        else if (IsFull)
            return false;
        else if (CurrentCount + count > MaxCount)
            CurrentCount = MaxCount;
        else
            CurrentCount += count;

        return true;
    }
}
