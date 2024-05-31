using Clones.Types;
using System;
using UnityEngine;
using Player = Clones.Character.Player.Player;

namespace Clones.Biomes
{
    public class Biome : MonoBehaviour
    {
        public event Action<Biome> PlayerEntered;
        public event Action PlayerExited;

        public Player Player { get; private set; }
        public BiomeType Type { get; private set; }

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

        public void Init(BiomeType type) =>
            Type = type;

        protected virtual void OnPlayerEntered() { }

        protected virtual void OnPlayerExited() { }
    }
}
