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

        public void Disable()
        {
            _worldGenerator.TileCreated -= OnTileCreated;
            _worldGenerator.TileDestroyed -= OnTileDestroyed;
        }

        private void OnTileCreated(GameObject tile)
        {
            Biome biome = tile.GetComponentInChildren<Biome>();

            if (biome != null)
            {
                biome.PlayerEntered += OnPlayerEntered;
                biome.PlayerExited += OnPlayerExited;
            }
        }

        private void OnTileDestroyed(GameObject tile)
        {
            Biome biome = tile.GetComponentInChildren<Biome>();

            if (biome != null)
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