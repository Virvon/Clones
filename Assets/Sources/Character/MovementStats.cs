public class MovementStats
{
    public float MovementSpeed => _movementSpeed * (1 - _slowDownPercent);
    public float AttakcSpeed => _attackSpeed;

    private float _movementSpeed;
    private float _attackSpeed;
    private float _slowDownPercent;

    public MovementStats(float movementSpeed, float attakcSpeed)
    {
        _movementSpeed = movementSpeed;
        _attackSpeed = attakcSpeed;
    }

    public void Freez(float movementSpeed, float attackSpeed)
    {
        _movementSpeed = movementSpeed;
        _attackSpeed = attackSpeed;
    }

    public void SlowDown(float percent) => _slowDownPercent = percent;
}
