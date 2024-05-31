using Clones.BiomeEffects;
using UnityEngine;

namespace Clones.Character.Player
{
    public class PlayerFreezing : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private FreezbarReporter _freezbarReporter;
        [SerializeField] private FreezingScreenReporter _freezingScreenReporter;

        private Freezing _freezing;

        public void Freez()
        {
            if (_freezing == null)
            {
                _freezing = new Freezing(_player.StatsProvider, _player.GetComponent<PlayerHealth>(), _player);

                _player.Decorate(_freezing);
                _freezbarReporter.SetFreezing(_freezing);
                _freezingScreenReporter.SetFreezing(_freezing);
            }

            _freezing.Freez();
        }

        public void Defrost() =>
            _freezing.Defrost();
    }
}