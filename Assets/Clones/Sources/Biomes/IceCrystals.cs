namespace Clones.Biomes
{
    public class IceCrystals : Biome
    {
        private PlayerFreezing _playerFreezing;

        protected override void OnPlayerEntered()
        {
            _playerFreezing = Player.GetComponentInChildren<PlayerFreezing>();

            _playerFreezing.Freez();
        }

        protected override void OnPlayerExited() => 
            _playerFreezing.Defrost();
    }
}
