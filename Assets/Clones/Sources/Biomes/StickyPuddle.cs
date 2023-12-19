using Clones.StateMachine;
using System;
using UnityEngine;

namespace Clones.Biomes
{
    [RequireComponent(typeof(Collider))]
    public class StickyPuddle : MonoBehaviour
    {
        [SerializeField, Range(0, 100)] private int _movementSpeedPercent;

        private float _movementSpeed;
        private IStatsProvider _previousStatsProvider;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _previousStatsProvider = player.StatsProvider;
                player.Decorate(new InstantDeceleration(player.StatsProvider, _movementSpeedPercent));
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                player.Decorate(_previousStatsProvider);
        }
    }
}
