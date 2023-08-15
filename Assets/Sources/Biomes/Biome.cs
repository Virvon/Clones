using Clones.Data;
using System;
using UnityEngine;

namespace Clones.Biomes
{
    [RequireComponent(typeof(Collider))]
    public class Biome : MonoBehaviour
    {
        [SerializeField] private BiomeData _biomeData;

        public BiomeData BiomeData => _biomeData;

        public Player Player { get; private set; }

        public event Action<Biome> PlayerEntered;
        public event Action PlayerExited;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                Player = player;
                PlayerEntered?.Invoke(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                Player = null;
                PlayerExited?.Invoke();
            }
        }
    }
}
