public class Stats
{
    public int Health { get; private set; }
    public int Damage { get; private set; }
    public float AttackSpeed { get; private set; }

    public Stats(int health, int damage, float attackSpeed)
    {
        Health = health;
        Damage = damage;
        AttackSpeed = attackSpeed;
    }
}
