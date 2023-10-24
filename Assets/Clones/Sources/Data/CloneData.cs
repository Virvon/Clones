using Clones.StaticData;
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
        public int UpgradePrice;
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

        public void Upgrade(int health, int damage, int upgradePrice)
        {
            Health += health;
            Damage += damage;
            UpgradePrice += upgradePrice;
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
