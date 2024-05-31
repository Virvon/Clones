using System;

namespace Clones.Character
{
    public interface IHealthChanger
    {
        public int Health { get; }
        public event Action HealthChanged;
    }
}
