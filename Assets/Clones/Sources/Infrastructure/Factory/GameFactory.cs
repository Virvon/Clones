using UnityEngine;

namespace Clones.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }

        public void CreateHud() => 
            _assets.Instantiate(AssetPath.HudPath);

        public GameObject CreatePlayer()=>
            _assets.Instantiate(AssetPath.PlayerPath);
    }
}