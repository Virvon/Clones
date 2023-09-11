using Clones.StaticData;
using System;
using UnityEngine;

namespace Clones.Biomes
{
    [RequireComponent(typeof(Collider))]
    public class Biome : MonoBehaviour
    {
        [SerializeField] private BiomeStaticData _biomeData;

        public BiomeStaticData BiomeData => _biomeData;

        public Player Player { get; private set; }

        public event Action<Biome> PlayerEntered;
        public event Action PlayerExited;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                Player = player;
                PlayerEntered?.Invoke(this);
                OnPlayerEntered();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                Player = null;
                PlayerExited?.Invoke();
                OnPlayerExited();
            }
        }

        protected virtual void OnPlayerEntered() { }

        protected virtual void OnPlayerExited() { }
    }
}
