using Clones.Data;
using UnityEngine;

namespace Clones.Infrastructure
{
    public class StaticDataService : IStaticDataService
    {
        private const string WorldGeneratorStaticDataPath = "StaticData/World/WorldGenerator";

        public WorldGeneratorData GetWorldGeneratorData() =>
            Resources.Load<WorldGeneratorData>(WorldGeneratorStaticDataPath);
    }
}