using Clones.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Clones.Biomes
{
    public class CurrentBiome : MonoBehaviour
    {
        [SerializeField] private WorldGenerator _worldGenerator;
        [SerializeField] private BiomeData _defaultBiomeData;

        public BiomeData BiomeData { get; private set; }

        private void OnEnable()
        {
            BiomeData = _defaultBiomeData;
            _worldGenerator.TilesGenerated += OnTilesGenerated;
            _worldGenerator.TilesDiactivated += OnTilesDeactivated;
        }

        private void OnDisable()
        {
            _worldGenerator.TilesGenerated -= OnTilesGenerated;
            _worldGenerator.TilesDiactivated -= OnTilesDeactivated;
        }

        private void OnTilesGenerated(IReadOnlyList<GeneratorObjects> generatorsObjects)
        {
            foreach (var generator in generatorsObjects)
            {
                Biome[] biomes = generator.GetComponentsInChildren<Biome>();

                foreach (var biome in biomes)
                {
                    biome.PlayerEntered += OnPlayerEntered;
                    biome.PlayerExited += OnPlayerExited;
                }    
            }
        }

        private void OnTilesDeactivated(IReadOnlyList<GeneratorObjects> generatorsObjects)
        {
            foreach (var generator in generatorsObjects)
            {
                Biome[] biomes = generator.GetComponentsInChildren<Biome>();

                foreach (var biome in biomes)
                {
                    biome.PlayerEntered += OnPlayerEntered;
                    biome.PlayerExited += OnPlayerExited;
                }
            }
        }

        private void OnPlayerEntered(Biome biome) => BiomeData = biome.BiomeData;

        private void OnPlayerExited() => BiomeData = _defaultBiomeData;
    }
}
