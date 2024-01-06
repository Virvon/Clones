using Clones.Types;
using System;

namespace Clones.Data
{
    [Serializable]
    public class CloneData
    {
        private const int StartLevel = 1;

        public CloneType Type;
        public int Health;
        public int Damage;
        public float AttackCooldown;
        public int UpgradePrice;
        public float ResourceMultiplier;
        public string DisuseEndDate;
        public int Level;

        public bool IsUsed => GetDisuseEndDate() - DateTime.Now > TimeSpan.Zero;

        public event Action Upgraded;

        public CloneData(CloneType type, int health, int damage, int upgradePrice)
        {
            Type = type;
            Health = health;
            Damage = damage;
            UpgradePrice = upgradePrice;
            Level = StartLevel;

            DisuseEndDate = DateTime.MinValue.ToString();
        }

        public void Upgrade(int health, int damage, float attackCooldown, float resourceMultiplier, int price)
        {
            Health += health;
            Damage += damage;
            AttackCooldown -= attackCooldown;
            ResourceMultiplier += resourceMultiplier;
            UpgradePrice += price;
            Level++;

            Upgraded?.Invoke();
        }

        public void Use(int disuseTime)
        {
            DateTime disuseEndDate = DateTime.Now;
            disuseEndDate = disuseEndDate.AddSeconds(disuseTime);
            DisuseEndDate = disuseEndDate.ToString();
        }

        public DateTime GetDisuseEndDate() => 
            DateTime.Parse(DisuseEndDate);
    }
}
