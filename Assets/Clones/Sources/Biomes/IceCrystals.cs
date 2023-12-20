using Clones.Services;

namespace Clones.Biomes
{
    public class IceCrystals : Biome, ICoroutineRunner
    {
        private static Freezing _freezing;

        protected override void OnPlayerEntered()
        {
            if(_freezing == null)
            {
                _freezing = new Freezing(Player.StatsProvider, Player.GetComponent<PlayerHealth>());
                Player.Decorate(_freezing);

                Player
                    .GetComponentInChildren<FreezbarReporter>()
                    .SetFreezing(_freezing);
            }

            _freezing.SetCoroutineRunner(this);
            _freezing.Freez();
        }

        protected override void OnPlayerExited() =>
            _freezing.Defrost();
    }
}
