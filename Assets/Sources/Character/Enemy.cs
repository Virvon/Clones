public class Enemy : Clone
{
    public TargetArea TargetArea { get; private set; }
    public Clone Target { get; private set; }

    public void Init(Clone target, TargetArea targetArea)
    {
        Target = target;
        TargetArea = targetArea;
    }
}
