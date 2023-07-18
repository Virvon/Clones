using Clones.Data;
using System;
using UnityEngine;

namespace Clones.Biomes
{
    [RequireComponent(typeof(Collider))]
    public abstract class Biome : MonoBehaviour
    {
        [SerializeField] private BiomeData _biomeData;

        public BiomeData BiomeData => _biomeData;

        public event Action<Biome> PlayerEntered;
        public event Action PlayerExited;

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
