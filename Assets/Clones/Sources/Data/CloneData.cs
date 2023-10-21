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
        public string DisuseEndDate;

        public event Action Upgraded;

        public CloneData(CloneType type, int health, int damage, int upgradePrice)
        {
            Type = type;
            Health = health;
            Damage = damage;
            UpgradePrice = upgradePrice;

            IsUsed = false;
        }

        public void Upgrade(int health, int damage, int upgradePrice)
        {
            Health += health;
            Damage += damage;
            UpgradePrice += upgradePrice;

            Upgraded?.Invoke();
        }

        public void Use(int disuseTime)
        {
            IsUsed = true;
            
            DateTime disuseEndDate = DateTime.Now;
            disuseEndDate = disuseEndDate.AddSeconds(disuseTime);
            DisuseEndDate = disuseEndDate.ToString();
        }

        public DateTime GetDisuseEndDate() => 
            DateTime.Parse(DisuseEndDate);
    }
}
