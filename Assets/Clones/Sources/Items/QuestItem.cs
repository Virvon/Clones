using Clones.Services;
using Clones.StaticData;
using UnityEngine;

public class QuestItem : MonoBehaviour, IItem
{
    public QuestItemType Type { get; private set; }

    public void Init(QuestItemType type) =>
        Type = type;

    public void Accept(IItemVisitor visitor) => 
        visitor.Visit(this);
}