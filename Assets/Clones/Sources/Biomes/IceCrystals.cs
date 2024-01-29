namespace Clones.Biomes
{
    public class IceCrystals : Biome
    {
        private static Freezing _freezing;

        private PlayerHealth _playerHealth;

        protected override void OnPlayerEntered()
        {
            if(_freezing == null)
            {
                _freezing = new Freezing(Player.StatsProvider, Player.GetComponent<PlayerHealth>(), Player);
                Player.Decorate(_freezing);

                Player
                    .GetComponentInChildren<FreezbarReporter>()
                    .SetFreezing(_freezing);

                Player
                    .GetComponentInChildren<FreezingScreenReporter>()
                    .SetFreezing(_freezing);
            }

            _freezing.Freez();

            _playerHealth = Player.GetComponent<PlayerHealth>();
            _playerHealth.Died += OnDied;
        }


        protected override void OnPlayerExited()
        {
            _freezing.Defrost();
            _playerHealth.Died -= OnDied;
        }

        private void OnDied(IDamageable obj)
        {
            _freezing = null;
            _playerHealth.Died -= OnDied;
        }
    }
}
