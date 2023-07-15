using System.Collections.Generic;
using UnityEngine;

namespace Clones.Biomes
{
    public class CurrentBiome : MonoBehaviour
    {
        [SerializeField] private BiomeType _defaultBiome;
        [SerializeField] private WorldGenerator _worldGenerator;

        public BiomeType Biome { get; private set; }

        private void OnEnable()
        {
            Biome = _defaultBiome;

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

        private void OnPlayerEntered(Biome biome)
        {
            Biome = biome.Type;
        }

        private void OnPlayerExited()
        {
            Biome = _defaultBiome;
        }
    }
}
