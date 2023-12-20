namespace Clones.Biomes
{
    public class IceCrystals : Biome
    {
        private static Freezing _freezing;

        protected override void OnPlayerEntered()
        {
            if(_freezing == null)
            {
                _freezing = new Freezing(Player.StatsProvider, Player.GetComponent<PlayerHealth>(), Player);
                Player.Decorate(_freezing);

                Player
                    .GetComponentInChildren<FreezbarReporter>()
                    .SetFreezing(_freezing);
            }

            _freezing.Freez();
        }

        protected override void OnPlayerExited() =>
            _freezing.Defrost();
    }
}
