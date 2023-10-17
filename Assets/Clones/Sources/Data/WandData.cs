using Clones.StaticData;
using System;

namespace Clones.Data
{
    [Serializable]
    public class WandData
    {
        public WandType Type;
        public int Damage;
        public float Cooldown;

        public WandData(WandType type, int damage, float cooldown)
        {
            Type = type;
            Damage = damage;
            Cooldown = cooldown;
        }
    }
}
