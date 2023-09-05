namespace Clones.Biomes
{
    public class IceCrystals : Biome
    {
        private Freezing _freezing;

        private void OnEnable()
        {
            PlayerEntered += OnPlayerEntered;
            PlayerExited += OnPlayerExited;
        }

        private void OnDisable()
        {
            PlayerEntered -= OnPlayerEntered;
            PlayerExited -= OnPlayerExited;
        }

        private void OnPlayerEntered(Biome biome)
        {
            _freezing = Player.GetComponent<Freezing>();
            _freezing.Freez(1, 15);
        }

        private void OnPlayerExited()
        {
            _freezing.Freez(0, 5);
        }
    }
}
