using System;

namespace Clones.Character
{
    public interface IHealthChanger
    {
        public event Action HealthChanged;
        public int Health { get; }
    }
}
