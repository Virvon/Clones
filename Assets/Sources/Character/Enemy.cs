using UnityEngine;

public class Enemy : Character
{
    public TargetArea TargetArea { get; private set; }
    public Character Target { get; private set; }

    public void Init(Character target, TargetArea targetArea)
    {
        Target = target;
        TargetArea = targetArea;
    }
}
