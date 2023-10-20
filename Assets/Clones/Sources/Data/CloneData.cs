using Clones.StaticData;
using System;

namespace Clones.Data
{
    [Serializable]
    public class CloneData
    {
        public CloneType Type;
        public int Health;
        public int Damage;
        public int UpgradePrice;
        public bool IsUsed;
        public DateTime UseDate;

        public event Action Upgraded;

        public CloneData(CloneType type, int health, int damage, int upgradePrice)
        {
            Type = type;
            Health = health;
            Damage = damage;
            UpgradePrice = upgradePrice;
        }

        public void Upgrade(int health, int damage, int upgradePrice)
        {
            Health += health;
            Damage += damage;
            UpgradePrice += upgradePrice;

            Upgraded?.Invoke();
        }

        public void Use()
        {
            IsUsed = true;
            UseDate = DateTime.Now;
        }
    }
}
