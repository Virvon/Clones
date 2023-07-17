using System;
using UnityEngine;

namespace Clones.Biomes
{
    [RequireComponent(typeof(Collider))]
    public abstract class Biome : MonoBehaviour
    {
        public event Action<Biome> PlayerEntered;
        public event Action PlayerExited;

        public abstract BiomeType Type { get;}

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                PlayerEntered?.Invoke(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                PlayerExited?.Invoke();
            }
        }
    }
}
