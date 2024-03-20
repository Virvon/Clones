using Clones.Biomes;
using Clones.Types;
using UnityEngine;

namespace Clones.GameLogic
{
    public class CurrentBiome : ICurrentBiome
    {
        private const BiomeType DefaultType = BiomeType.Ground;

        private readonly WorldGenerator _worldGenerator;

        public BiomeType Type { get; private set; }

        public CurrentBiome(WorldGenerator worldGenerator)
        {
            _worldGenerator = worldGenerator;

            _worldGenerator.TileCreated += OnTileCreated;
            _worldGenerator.TileDestroyed += OnTileDestroyed;
        }

        public void OnDisable()
        {
            _worldGenerator.TileCreated -= OnTileCreated;
            _worldGenerator.TileDestroyed -= OnTileDestroyed;
        }

        private void OnTileCreated(GameObject tile)
        {
            if (tile.TryGetComponent(out Biome biome))
            {
                biome.PlayerEntered += OnPlayerEntered;
                biome.PlayerExited += OnPlayerExited;
            }
        }

        private void OnTileDestroyed(GameObject tile)
        {
            if (tile.TryGetComponent(out Biome biome))
            {
                biome.PlayerEntered -= OnPlayerEntered;
                biome.PlayerExited -= OnPlayerExited;
            }
        }

        private void OnPlayerEntered(Biome biome) => 
            Type = biome.Type;

        private void OnPlayerExited() => 
            Type = DefaultType;
    }
}